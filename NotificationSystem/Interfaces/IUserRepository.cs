using NotificationSystem.Models;

namespace NotificationSystem.Interfaces
{
    // Interface for User Repository
    internal interface IUserRepository
    {
        public User AddUser(User user);
        public User? FindUser(string email);
        public User? FindUserById(string id);
        public bool UpdateUser(string email, User newUpdateUser);
        public User? DeleteUser(string email);
    }
}