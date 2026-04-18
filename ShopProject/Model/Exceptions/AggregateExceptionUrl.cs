using ShopProject.Model.Domain.Notification;
using ShopProject.Services.Infrastructure.Exception.Interface;
using ShopProject.Services.Infrastructure.Mediator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Exceptions
{
    internal class AggregateExceptionUrl : AggregateException , IException
    {
        public AggregateExceptionUrl() : base(string.Empty) { }
        public AggregateExceptionUrl(string message) : base(message) { }
        public INotification Result => Notification.Error(nameof(AggregateExceptionUrl), this.Message, Enum.NotificationPersistence.None, false);
    }
}
