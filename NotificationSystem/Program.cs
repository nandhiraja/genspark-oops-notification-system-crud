using System.Xml;
using NotificationSystem.Repositories;
using NotificationSystem.Senders;
using NotificationSystem.Services;
using NotificationSystem.Models;

namespace NotificationSystem
{
    internal class Program
    {         
        UserService _userService;
        NotificationService _notificationService;

        public Program()
        {
            var userRepository = new UserRepository();
            var messageRepository = new MessageRepository();
            var emailSender = new EmailNotificationSender();
            var smsSender = new SMSNotificationSender();
            var notificationFactory = new NotificationFactory(emailSender, smsSender);

            _userService = new UserService(userRepository);
            _notificationService = new NotificationService(userRepository, messageRepository, notificationFactory);
        }

        void Run()
        {
            Console.WriteLine("Welcome to Notification service:\n");
            while(true){
                Console.WriteLine("\nEnter your Preference:\n1.User Profile\n2.Send Message\n3.Exit");
                string userPrefOption = Console.ReadLine()??"";
                if (userPrefOption == "1")
                {               
                    Console.WriteLine("\nChoose: \n1.Create New profile\n2.Edit Profile\n3.Exit");
                    string userProfilePref = Console.ReadLine()??"";
                    if(userProfilePref == "1")
                    {
                       _userService.CreateUserProfile();
                    }
                    else if(userProfilePref=="2")
                    {
                       Console.Write("Please Enter EmailId : ");
                       string useEmailId = Console.ReadLine()??"";
                        if (useEmailId != "")
                        {
                          _userService.EditUser(useEmailId);
                        }
                        else
                        {
                            Console.WriteLine("Sorry Can't find email");
                        }
                    }
                }
                else if(userPrefOption=="2")
                {
                    Console.WriteLine("\n---------------------------------------- Login ----------------------------------\n");
                    Console.Write("Please enter your email : ");
                    string loginEmail = Console.ReadLine()??"";

                    User? currentUser = _notificationService.LoginUser(loginEmail);
                    if (currentUser == null)
                    {
                        Console.WriteLine($"Unable to login email can't find user.. Email : {loginEmail}");
                        continue;
                    }

                    Console.WriteLine("\n-------------------------Message area -----------------------\n");
                    Console.WriteLine($"Sender : {currentUser.Email}");
                    Console.Write("Please enter receiver Email : ");
                    string receiverEmail = Console.ReadLine()??"";
                    
                    User? receiver = _notificationService._GetUser(receiverEmail);
                    if(receiver == null)
                    {
                        Console.WriteLine("Receiver not found");
                        continue;
                    }

                    Console.WriteLine($"Please message for {receiver.UserName} :  ");
                    string userMessage =  Console.ReadLine()??"";

                    Console.WriteLine("Do you need to push notification via: ");
                    Console.WriteLine("\n1.Email notification\n2.SMS notification\n3.Both");
                    string userPrefNotification = Console.ReadLine()??"";

                    _notificationService.ProcessNotification(currentUser, receiver, userMessage, userPrefNotification);
                }
                else if(userPrefOption=="3")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Please Enter valid option");
                }
            }
        }
        
        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}