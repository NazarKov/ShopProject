using ShopProject.Controls.MessegeBox.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Control.Interface
{
    internal interface IMessageBoxControlService
    {
        public Task<bool> Show(string message, string title = "Повідомлення", MessageBoxType type = MessageBoxType.Information, string shadowPage = "");
    }
}
