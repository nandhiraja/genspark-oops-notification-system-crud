using NotificationSystem.Interfaces;
using NotificationSystem.Models;

namespace NotificationSystem.Repositories
{
    // Repository for managing messages
    internal class MessageRepository : IMessageRepository
    {
        static Dictionary<string, List<Message>> _messageDataBase = new Dictionary<string, List<Message>>();

        public void AddUserMessage(User user, Message message)
        {   
            if(!_messageDataBase.ContainsKey(user.Id))  // check for new user
            {
                _messageDataBase.Add(user.Id, new List<Message>());
            }
            _messageDataBase[user.Id].Add(message);
        }

        public List<Message> GetUserMessages(User user)
        {
            if(_messageDataBase.ContainsKey(user.Id)) // check user send any earlier messages
            {
                return _messageDataBase[user.Id];
            }
            return new List<Message>();
        }

        public bool UpdateUserMessages(User user, Message updatedMessage)
        {
            if(_messageDataBase.ContainsKey(user.Id))
            {   
                List<Message> data = _messageDataBase[user.Id];
                for(int i = 0; i < data.Count; i++)
                {   
                    if(data[i].MessageId == updatedMessage.MessageId) // find the exist message
                    {
                        data[i] = updatedMessage;
                        return true;
                    }
                }
            }
            return false;
        }
          
        public Message? DeleteUserMessages(User user, Message message)
        {
            if(_messageDataBase.ContainsKey(user.Id))
            {   
                List<Message> data = _messageDataBase[user.Id];
                for(int i = 0; i < data.Count; i++)
                {   
                    if(data[i].MessageId == message.MessageId)
                    {
                        Message deletedMsg = data[i];
                        data.RemoveAt(i);   // remove that message user choosed
                        return deletedMsg;
                    }
                }
            }
            return null;
        }
    }
}