using NotificationSystem.Models;

namespace NotificationSystem.Interfaces
{
    // Interface for Message Repository
    internal interface IMessageRepository
    {
        public void AddUserMessage(User user, Message message);
        public List<Message> GetUserMessages(User user);
        public bool UpdateUserMessages(User user, Message updatedMessage);
        public Message? DeleteUserMessages(User user , Message message);
    }
}