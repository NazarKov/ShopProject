using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.ControlWindow.Interface
{
    internal interface IControlWindowService
    {
        public void ActivateMainWindow();
        public void CloseSecondaryWindows();
    }
}
