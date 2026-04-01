using ShopProject.Core.Mvvm.ExceptionServise.Interface;
using ShopProject.Core.Mvvm.Mediator.Interface;
using ShopProject.Model.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Exceptions
{
    internal class ExceptionGeneral : Exception, IException
    {
        public ExceptionGeneral() : base(string.Empty) { }
        public ExceptionGeneral(string message) : base(message) { }

        public INotification Result => Notification.Error(nameof(ExceptionGeneral), this.Message);
    }
}
