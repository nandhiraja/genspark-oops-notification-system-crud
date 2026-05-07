using System.Data.Common;

namespace NotificationSystem.Models
{
    internal class User
    {   
        public string Id {get; set;}  = string.Empty; 
        public string UserName {get;set;} = string.Empty;
        public string Email {get;set;} = string.Empty;
        public string PhoneNumber {get;set;} = string.Empty;

        public User()
        {
            
        }
        public User(string id,string userName,string email, string phoneNumber)
        {   this.Id = id;
            this.UserName =userName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;

        }

    }
}