using System.Reflection.Metadata.Ecma335;
using NotificationSystem.Interfaces;
using NotificationSystem.Models;
using NotificationSystem.Repositories;

namespace NotificationSystem.Services
{
    
    internal class NotificationService
    {   
        static string _messageId = "1"; 
        static UserRepository _userRepository = new UserRepository();
        static MessageRepository _messageRepository = new MessageRepository();
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
            _messageRepository.AddUserMessage(currentUser,newMessage);
            PrintNotification(newMessage);

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
            Console.WriteLine("\n================ Notification send Successfully ============================\n");

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