using NotificationSystem.Interfaces;
using NotificationSystem.Models;

namespace NotificationSystem.Repositories
{
    internal class MessageRepository:IMessageRepository
    {
        static Dictionary<User,List<Message>> _messageDataBase = new Dictionary<User, List<Message>>();

        public void AddUserMessage(User user, Message message)
        {   
            if(!_messageDataBase.ContainsKey(user))
            {
                _messageDataBase.Add(user,new List<Message>());  //. add new list for new user for db
            }
            _messageDataBase[user].Add(message);   // add user with message
        
        }

        public List<Message> GetUserMessages(User user)
        {
            if(_messageDataBase.ContainsKey(user)) // check user send any message previously
            {
                return _messageDataBase[user];
            }
            return new List<Message>();    // if no user send any previous message return empty list
        }

         public bool UpdateUserMessages(User user, Message previousMessage , Message currentMessage)
          {
            if(_messageDataBase.ContainsKey(user)) // check user send any message previously
            {   List<Message> data = _messageDataBase[user];
                int range = data.Count;
                for( int i =0; i<range ;i++)
                {   Message message = data[i];
                    if(message.MessageId == previousMessage.MessageId)
                    {
                        data[i] = currentMessage;
                        
                        _messageDataBase[user] = data;
                        return true;   // if message is update successfully return true
                    }
                }
            }
            return false;  // if message is not found;
          }
          
          public Message? DeleteUserMessages(User user , Message message)
          {
            if(_messageDataBase.ContainsKey(user)) // check user send any message previously
            {   List<Message> data = _messageDataBase[user];
                int range = data.Count;
                for( int i =0; i<range ;i++)
                {   Message currentMessage = data[i];
                    if(currentMessage.MessageId == message.MessageId)
                    {
                        data.RemoveAt(i);
                        
                        _messageDataBase[user] = data;
                        return currentMessage;   // if message is delete successfully return message
                    }
                }
            }
            return null;  // no message delete or found ;
          }
          

    }
}