using DocumentFormat.OpenXml.Vml.Spreadsheet;
using ShopProject.Model.Enum;
using ShopProject.Services.Infrastructure.Mediator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.Notification
{
    public class Notification :INotification
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public TypeNotification Type { get; set; }
        public NotificationPersistence NotificationPersistence { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsVisible { get; set; }
        
        private static Notification GetNewNotification(string title, string content, TypeNotification typeNotification ,bool isVisible = true, NotificationPersistence notificationPersistence = NotificationPersistence.None ) 
        {
            return new Notification()
            {
                Content = content,
                DateTime = DateTime.Now,
                IsVisible = isVisible,
                NotificationPersistence = notificationPersistence,
                Title = title,
                Type = typeNotification
            };
        }

        public static Notification Succes(string title, string content,  NotificationPersistence notificationPersistence = NotificationPersistence.None,bool isVisible = true)
            => GetNewNotification(title, content, TypeNotification.Succes, isVisible, notificationPersistence);
        public static Notification Error(string title, string content, NotificationPersistence notificationPersistence = NotificationPersistence.None, bool isVisible = true)
             => GetNewNotification(title, content, TypeNotification.Error, isVisible, notificationPersistence);
        public static Notification Info(string title, string content, NotificationPersistence notificationPersistence = NotificationPersistence.None, bool isVisible = true)
            => GetNewNotification(title, content, TypeNotification.Info, isVisible, notificationPersistence);
        public static Notification Warning(string title, string content, NotificationPersistence notificationPersistence = NotificationPersistence.None, bool isVisible = true)
            => GetNewNotification(title, content, TypeNotification.Warning, isVisible, notificationPersistence);

        internal static INotification Error(string v1, string message, object none, bool v2)
        {
            throw new NotImplementedException();
        }
    }
}
