using Microsoft.VisualBasic;
using NotificationSystem.Interfaces;
using NotificationSystem.Models;

namespace NotificationSystem.Repositories
{
    internal class UserRepository: IUserRepository
    {
        static List<User> _userDataBase = new List<User>();

        public User AddUser(User user)
        {   
            _userDataBase.Add(user);
            return user;
        }
        public User? FindUser(string email)
        {
            foreach(var currentUser in _userDataBase)
            {
                if(currentUser.Email == email)   // find user with user id
                {
                  return currentUser;   
                }
            }
            return null;
        }

        public bool UpdateUser(string email, User newUpdateUser)
        {
            int length =_userDataBase.Count;

            for (int i =0;i<length;i++)
            {
                if(_userDataBase[i].Email==email)
                {
                    _userDataBase[i] = newUpdateUser;
                    return true;
                }
                
            }
            return false;
            
        }


        public User? DeleteUser(string email)
        {
            int length =_userDataBase.Count;

            for (int i =0;i<length;i++)
            {
                if(_userDataBase[i].Email==email)
                {   User currentUser = _userDataBase[i];
                    _userDataBase.RemoveAt(i);
                    return currentUser;
                }
                
            }
            return null;
            
        }

    }
}