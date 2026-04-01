using ShopProject.Core.Mvvm.Mediator.Interface;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Core.Mvvm.ExceptionServise.Interface
{
    public interface IException
    {
        public abstract INotification Result { get; } 
    }
}
