using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Enum;
using ShopProject.Services.Infrastructure.Exception.Interface;
using ShopProject.Services.Infrastructure.Mediator.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Exceptions
{
    internal class AuthorizationException : Exception , IException
    {
        public AuthorizationException() : base(string.Empty) { }
        public AuthorizationException(string message) : base(message) { }
        public INotification Result => Notification.Error(nameof(СonnectionException), this.Message, NotificationPersistence.None, false);
    }
}
