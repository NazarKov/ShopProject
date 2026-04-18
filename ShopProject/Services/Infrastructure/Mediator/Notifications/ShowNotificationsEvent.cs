using ShopProject.Services.Infrastructure.Mediator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.Mediator.Notifications
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
