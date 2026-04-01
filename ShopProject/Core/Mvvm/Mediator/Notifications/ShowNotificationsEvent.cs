
using ShopProject.Core.Mvvm.Mediator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Core.Mvvm.Mediator.Notifications
{
    public class ShowNotificationEvent
    {
        public INotification Notification { get; } 

        public ShowNotificationEvent(INotification notification)
        {
            Notification = notification;
        }
    }
}
