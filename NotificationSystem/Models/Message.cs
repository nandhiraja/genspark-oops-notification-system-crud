namespace NotificationSystem.Models
{   
    internal enum NotificationType{
        Email,
        SMS
    }
    internal class Message
    {
        public string MessageId {get; set;} = string.Empty;
        public string SenderId {get; set;} = string.Empty;
        public string ReceiverId {get; set;} = string.Empty;
        public string MessageContent {get; set;} = string.Empty;
        public DateTime Date {get; set;}
        public NotificationType NotificationMode {get; set;}


        public Message()
        {
            Date = DateTime.UtcNow;
        }
        public Message(string senderId, string receiverId, string messageContent)
        {
            this.SenderId =senderId;
            this.ReceiverId =receiverId;
            this.MessageContent=messageContent;
            Date = DateTime.UtcNow;
        }
    }
}