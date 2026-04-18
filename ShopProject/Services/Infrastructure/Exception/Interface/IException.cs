using ShopProject.Services.Infrastructure.Mediator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.Exception.Interface
{
    public interface IException
    {
        public abstract INotification Result { get; } 
    }
}
