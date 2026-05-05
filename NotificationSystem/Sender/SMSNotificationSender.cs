using NotificationSystem.Interfaces;
using NotificationSystem.Models;

namespace NotificationSystem.Sender
{
    internal class SMSNotificationSender : INotification
    {
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
            Console.WriteLine("\n================ SMS Notification send Successfully =======================\n");
            Console.WriteLine($"Sender: {notificationMessage.SenderId}\nReceiver: {notificationMessage.ReceiverId}");
            Console.WriteLine("\n------------------------------------------------------------------------------\n");

            Console.WriteLine($"Message: {notificationMessage.MessageContent}\n Date: {notificationMessage.Date}");
            Console.WriteLine("\n================ ==================================== =======================\n");

        }

    }
}