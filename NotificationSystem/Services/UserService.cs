using System.Diagnostics;
using NotificationSystem.Interfaces;
using NotificationSystem.Models;
using NotificationSystem.Repositories;
namespace NotificationSystem.Services
{
    /// <summary>
    /// Service for handling user profiles.
    /// Decoupled from concrete implementations using Dependency Injection.
    /// </summary>
    internal class UserService
    {   
        private IUserRepository _userRepository;
        static string _userId = "0";

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CreateUserProfile()
        {   
            string id = _GenerateUserId();

            Console.WriteLine("\n========================= Create Your Profile =============================\n");

            Console.Write("Please enter your full name : ");
            string userName = Console.ReadLine()??"";
            Console.WriteLine("");
            Console.Write("Please enter your EmailId: ");
            string userEmail = Console.ReadLine()??"";
            Console.WriteLine("");
            Console.Write("Please enter your PhoneNumber: ");

            string userPhoneNo = Console.ReadLine()??"";
            
            Console.WriteLine("\n============================== Thank You ==================================\n");

            User newUser =  new User(id,userName,userEmail,userPhoneNo);
            _userRepository.AddUser(newUser);
            PrintUser(newUser);
            return newUser;
        }
        public User? EditUser(string email)
        {
            User? currentUser = _userRepository.FindUser(email);
            if(currentUser!=null)
            {
            Console.WriteLine("\n========================= Edit Your Profile =============================\n");
            while(true)
            {
            Console.WriteLine("Please enter what you need to edit:\n1.Name\n2.Email\n3.PhoneNo\n4.Save & Exit\n");
            string editSection =  Console.ReadLine()??"";
            if(editSection =="1")
                {
                    Console.WriteLine($"Current Name: {currentUser.UserName}");
                    Console.Write("New Name : ");
                    string newName = Console.ReadLine()??"";
                    if (newName != "")
                    {
                        currentUser.UserName = newName;
                    }
                    else
                        {
                            Console.WriteLine("Name not be empty..");
                        }
                }
            else if (editSection =="2")
                {
                    Console.WriteLine($"Current Email: {currentUser.Email}");
                    Console.Write("New Email : ");
                    string newEmail = Console.ReadLine()??"";
                    if (newEmail != "" && newEmail.Contains("@"))
                    {
                        currentUser.Email = newEmail;
                    }
                    else
                        {
                            Console.WriteLine("Email-Id is invalid to update....");
                        }
                }
            else if (editSection =="3")
                {
                    {
                    Console.WriteLine($"Current Phone Number: {currentUser.PhoneNumber}");
                    Console.Write("New PhoneNo : ");
                    string newPhoneNumber = Console.ReadLine()??"";
                    if (newPhoneNumber != "" )
                    {
                        currentUser.PhoneNumber = newPhoneNumber;
                    }
                    else
                        {
                            Console.WriteLine("PhoneNo is invalid to update....");
                        }
                }
                }
            else if (editSection =="4")
                {
                    Console.WriteLine("\n============================== Thank You ==================================\n");
                    PrintUser(currentUser);
                    return currentUser;
        
                }
            else 
                {
                    Console.WriteLine("Enter correct option please...");
                }

            }

            }
            return null;

        }
        string _GenerateUserId()
        {
            long previousId = Convert.ToInt64(_userId);
            string newId =  Convert.ToString(++previousId);
            _userId = newId;
            return newId;
        }
        void PrintUser(User user)
        {
            Console.WriteLine("User Details: ");
            Console.WriteLine($"User Id      : {user.Id} ");
            Console.WriteLine($"User Name    : {user.UserName} ");
            Console.WriteLine($"User Email   : {user.Email} ");
            Console.WriteLine($"User PhoneNo : {user.PhoneNumber} ");

        }

    }
}