using ShopProject.Core.Mvvm.ExceptionServise.Interface;
using ShopProject.Core.Mvvm.Mediator.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Enum;
using ShopProject.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Exception
{
    internal class СonnectionException : System.Exception , IException
    { 
        public СonnectionException() : base(string.Empty) { }
        public СonnectionException(string message) : base(message) { } 
        public INotification Result => Notification.Error(nameof(СonnectionException), this.Message,NotificationPersistence.None, false);
    }
}
