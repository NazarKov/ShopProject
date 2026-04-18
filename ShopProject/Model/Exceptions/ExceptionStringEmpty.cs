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
    public class ExceptionStringEmpty : Exception , IException
    {
        public ExceptionStringEmpty() : base(string.Empty) { }
        public ExceptionStringEmpty(string message) : base(message) { }

        public INotification Result => Notification.Error("Товар",this.Message , Enum.NotificationPersistence.None,false); 
    }
}
