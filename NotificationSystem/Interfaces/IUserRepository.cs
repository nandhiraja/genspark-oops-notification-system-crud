using NotificationSystem.Models;

namespace NotificationSystem.Interfaces
{
    internal interface IUserRepository
    {
        public User AddUser(User user);
        public User? FindUser(string email);
        public bool UpdateUser(string email, User newUpdateUser);
        public User? DeleteUser(string email);



    }
}