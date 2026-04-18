using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.Mediator.Interface
{
    public interface INotification
    {
        public string Title { get; set; } 
        public string Content { get; set; }
    }
}
