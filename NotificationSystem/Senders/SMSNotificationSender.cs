using NotificationSystem.Interfaces;
using NotificationSystem.Models;
using NotificationSystem.Repositories;

namespace NotificationSystem.Senders
{
    internal class SMSNotificationSender : INotification
    {
        UserRepository userRepository = new UserRepository();

        public bool Send(Message message)
        {   bool isSend =true;    // assume message send succesfully

            if(isSend)
              {   
                PrintNotification(message);
                return true;
              }   
            return false;
        }
        
        public void PrintNotification(Message notificationMessage)

        {
            User? sender = userRepository.FindUserById(notificationMessage.SenderId);
            User? receiver = userRepository.FindUserById(notificationMessage.ReceiverId);
            if(sender==null || receiver ==null)
            {
                return;    // if sender or receiver not found return no print happen
            }
            Console.WriteLine("\n================ SMS Notification send Successfully =======================\n");
            Console.WriteLine($"Sender Name: {sender.UserName}  | Receiver Name: {receiver.UserName}");

            Console.WriteLine($"Sender: {sender.PhoneNumber}\nReceiver: {receiver.PhoneNumber}");
            Console.WriteLine("\n------------------------------------------------------------------------------\n");

            Console.WriteLine($"Message: {notificationMessage.MessageContent}\n Date: {notificationMessage.Date}");
            Console.WriteLine("\n================ ==================================== =======================\n");

        }

    }
}