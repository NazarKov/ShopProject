using ShopProject.Services.Integration.Windows.WindowsService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Windows.WindowsService.Interface
{
    internal interface IWindowsServiceManager
    {
        public void CreateService(string pathService, string name);
        public void StartService();
        public void StopService();
        public void RestartService();
        public StatusService GetStatus();
        public void DeleteService();
    }
}
