using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Core.Mvvm.Mediator.Interface
{
    public interface INotification
    {
        public string Title { get; set; } 
        public string Content { get; set; }
    }
}
