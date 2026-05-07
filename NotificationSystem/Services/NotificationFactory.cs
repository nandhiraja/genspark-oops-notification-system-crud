using NotificationSystem.Interfaces;
using NotificationSystem.Senders;

namespace NotificationSystem.Services
{
    /// <summary>
    /// Factory class to create and return the appropriate notification sender based on user preference
    /// </summary>
    internal class NotificationFactory
    {
        private INotification _emailSender;
        private INotification _smsSender;

        public NotificationFactory(INotification emailSender, INotification smsSender)
        {
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        /// <summary>
        /// Returns a list of INotification senders based on the provided preference.
        /// </summary>
        public List<INotification> GetNotificationSenders(string preference)
        {
            List<INotification> senders = new List<INotification>();

            if (preference == "1")
            {
                senders.Add(_emailSender);
            }
            else if (preference == "2")
            {
                senders.Add(_smsSender);
            }
            else if (preference == "3")
            {
                senders.Add(_emailSender);
                senders.Add(_smsSender);
            }

            return senders;
        }
    }
}
