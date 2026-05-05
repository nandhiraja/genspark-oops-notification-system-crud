using NotificationSystem.Models;

namespace NotificationSystem.Interfaces

{
    internal interface INotification
    {
        public bool Send(Message message);

        public void PrintNotification(Message notificationMessage);


    }
}