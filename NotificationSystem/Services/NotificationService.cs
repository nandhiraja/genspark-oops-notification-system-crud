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
        static UserRepository _userRepository = new UserRepository();
        static MessageRepository _messageRepository = new MessageRepository();
        static EmailNotificationSender _emailNotificationSender =   new EmailNotificationSender();
        static SMSNotificationSender _sMSNotificationSender = new SMSNotificationSender();
        public void ProcessNotification()

        {   
            User? currentUser = LoginUser();
            if (currentUser == null)
            {
                return;
            }

            Message? newMessage = _SendNewMessage(currentUser);
            if (newMessage == null)
            {
              Console.WriteLine("Sorry. Can't send message...");
              return;
            }

            Console.WriteLine("Do you need to push notification via: ");
            Console.WriteLine("\n1.Email notification\n2.SMS notification\n3.Both");
            string userPrefNotification = Console.ReadLine()??"";
            if (userPrefNotification == "1")
            {
                Message emailMessage = _sendEmailNotification(newMessage);
                _messageRepository.AddUserMessage(currentUser,emailMessage);
            }
            else if (userPrefNotification == "2")
            {
                Message SMSMessage = _sendSMSNotification(newMessage);
                _messageRepository.AddUserMessage(currentUser,SMSMessage);
             }
            else if (userPrefNotification == "3")
            {
                Message emailMessage = _sendEmailNotification(newMessage);
                _messageRepository.AddUserMessage(currentUser,emailMessage);

                Message SMSMessage = _sendSMSNotification(newMessage);
                _messageRepository.AddUserMessage(currentUser,SMSMessage);

            }
            else
            {
                Console.WriteLine("Enter valid input");
            }


            return;
        }
       
        public User? LoginUser()
        {   
            
            Console.WriteLine("\n---------------------------------------- Login ----------------------------------\n");
            Console.Write("Please enter your email : ");
            string loginEmail = Console.ReadLine()??"";

            User? loginUser = _GetUser(loginEmail);
            if(loginUser!=null)
            {
                return loginUser;
            }
            Console.WriteLine($"Unable to login email can't find user.. Email : {loginEmail}");
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

        public Message? _SendNewMessage(User sender)
        {
            Console.WriteLine("\n-------------------------Message area -----------------------\n");
            Console.WriteLine($"Sender : {sender.Email}");
            Console.Write("Please enter receiver Email : ");
            User? receiver = _GetUser(Console.ReadLine()??"");
            if(receiver == null)
            {
                Console.WriteLine("Receiver not found");
                return null;
            }
            Console.WriteLine($"Please message for {receiver.UserName} :  ");
            string userMessage =  Console.ReadLine()??"";
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

        Message _sendSMSNotification(Message message)
        {
               message.NotificationMode = NotificationType.SMS;
               _sMSNotificationSender.Send(message);
            
               return message;
        }
         Message _sendEmailNotification(Message message)
        {
            message.NotificationMode = NotificationType.Email;
            _emailNotificationSender.Send(message);
               return message;
        }

    }
}