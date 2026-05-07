using System.Xml;
using NotificationSystem.Services;

namespace NotificationSystem
{
    internal class Program
    {         
        UserService _userService = new UserService();
        NotificationService _notificationService =   new NotificationService();

        public Program()
        {
            
        }

        void Run()
        {
            
            Console.WriteLine("Welcome to Notification service:\n");
            while(true){
            Console.WriteLine("\nEnter your Preference:\n1.create a profile\n2.Send Message\n3.Exit");
            string userPrefOption = Console.ReadLine()??"";
            if (userPrefOption == "1")
            {               
                Console.WriteLine("Choose: \n1.Create New profile\n2.Edit Profile\n3.Exit");
                string userProfilePref = Console.ReadLine()??"";
                if(userProfilePref == "1")
                    {
                       _userService.CreateUserProfile();
 
                    }
                else if(userProfilePref=="2")
                    {  Console.Write("Please Enter EmailId : ");
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
                _notificationService.ProcessNotification();
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