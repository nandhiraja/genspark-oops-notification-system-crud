using NotificationSystem.Models;

namespace NotificationSystem.Interfaces
{
    internal interface IMessageRepository
    {
        public void AddUserMessage(User user, Message message);
        public List<Message> GetUserMessages(User user);
        public bool UpdateUserMessages(User user, Message previousMessage , Message currentMessage);
        public Message? DeleteUserMessages(User user , Message message);

    }
}