using System.Reflection.Metadata.Ecma335;
using NotificationSystem.Interfaces;
using NotificationSystem.Models;
using NotificationSystem.Repositories;
using NotificationSystem.Senders;

namespace NotificationSystem.Services
{
    
    internal class NotificationService
    {   
        static string _messageId = "1"; 
        
        private IUserRepository _userRepository;
        private IMessageRepository _messageRepository;
        private NotificationFactory _notificationFactory;

        /// <summary>
        /// Constructor injection to decouple concrete classes.
        /// </summary>
        public NotificationService(IUserRepository userRepository, IMessageRepository messageRepository, NotificationFactory factory)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _notificationFactory = factory;
        }

        /// <summary>
        /// Processes the notification by taking sender, receiver, and message content then sending via the chosen mode.
        /// </summary>
        public void ProcessNotification(User currentUser, User receiver, string userMessage, string userPrefNotification)
        {   
            Message? newMessage = _SendNewMessage(currentUser, receiver, userMessage);
            if (newMessage == null)
            {
              Console.WriteLine("Sorry. Can't send message...");
              return;
            }

            var senders = _notificationFactory.GetNotificationSenders(userPrefNotification);
            
            if (senders.Count == 0)
            {
                Console.WriteLine("Enter valid input");
                return;
            }

            foreach (var sender in senders)
            {
                Message sendMsg = new Message(newMessage.MessageId, newMessage.SenderId, newMessage.ReceiverId, newMessage.MessageContent);
                
                if (sender is EmailNotificationSender) 
                {
                    sendMsg.NotificationMode = NotificationType.Email;
                }
                else if (sender is SMSNotificationSender)
                {
                    sendMsg.NotificationMode = NotificationType.SMS;
                }

                sender.Send(sendMsg);
                _messageRepository.AddUserMessage(currentUser, sendMsg);
            }
        }
       
        /// <summary>
        /// Authenticates the user based on the provided email.
        /// </summary>
        public User? LoginUser(string loginEmail)
        {   
            User? loginUser = _GetUser(loginEmail);
            if(loginUser!=null)
            {
                return loginUser;
            }
            return null;
        }

        public User? _GetUser(string email)
        {   
           if(email != "" && email.Contains("@"))
            {
                User? user = _userRepository.FindUser(email);
                if (user != null)
                    return user;
            }

            return null;
        }

        public Message? _SendNewMessage(User sender, User receiver, string userMessage)
        {
            string messageId = _GenerateMessageId();
            Message newMessage = new Message(messageId,sender.Id,receiver.Id,userMessage);
            return newMessage;
        }

        public void PrintNotification(Message notificationMessage)
        {
            Console.WriteLine($"\n================ {notificationMessage.NotificationMode} Notification send Successfully ============================\n");
            Console.WriteLine($"Sender : {notificationMessage.SenderId}");
            Console.WriteLine($"Receiver : {notificationMessage.ReceiverId}");
            Console.WriteLine($"Message : {notificationMessage.MessageContent}");
            Console.WriteLine($"Date : {notificationMessage.Date}");
            Console.WriteLine($"\n================ ===================================================================== ============================\n");
        }

        string _GenerateMessageId()
        {
            long previousId = Convert.ToInt64(_messageId);
            string newId =  Convert.ToString(++previousId);
            _messageId = newId;
            return newId;
        }
    }
}