using DocumentFormat.OpenXml.Wordprocessing;
using ShopProject.Model.Integration.Windows;
using ShopProject.Services.Integration.Windows.WindowsService.Helper;
using ShopProject.Services.Integration.Windows.WindowsService.Interface;
using ShopProject.Services.Modules.Setting;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Windows.WindowsService
{
    internal class WindowsServiceManager : IWindowsServiceManager
    {  
        private ServiceController _serviceController;
        private ISettingService _settingService;
        public WindowsServiceManager(ISettingService settingService) 
        {
            _settingService = settingService;
            var setting = _settingService.GetSetting<SettingWindowsService>();
            if (setting == null) 
            {
                setting = new SettingWindowsService();
                _serviceController = new ServiceController();
            }
            else
            {
                _serviceController = new ServiceController(setting.Name, Environment.MachineName);
            } 
        }
        public void CreateService(string pathService,string name)
        {

             Process.Start(new ProcessStartInfo
             {
                 FileName = "sc",
                 Arguments = $"create {name} binPath= \"{pathService}\" start= auto",
                 Verb = "runas",
                 UseShellExecute = true
             });
            _serviceController = new ServiceController(name,Environment.MachineName);
            _settingService.SetSetting<SettingWindowsService>(new SettingWindowsService() { Name = name });
        }

        public void StartService()
        {
            _serviceController.Refresh();
            if (_serviceController.Status != ServiceControllerStatus.Running)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "sc.exe",
                    Arguments = $"start \"{_serviceController.ServiceName}\"",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true
                });
            } 
        }
        public void StopService() 
        {
            _serviceController.Refresh();
            if (_serviceController.Status != ServiceControllerStatus.Stopped)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "sc.exe",
                    Arguments = $"stop \"{_serviceController.ServiceName}\"",
                    Verb = "runas",  
                    UseShellExecute = true,
                    CreateNoWindow = true,            
                });
            } 
        }
        public void RestartService()
        {
            _serviceController.Refresh();
            if (_serviceController.Status != ServiceControllerStatus.Stopped)
            {
                StopService();
            }
            StartService(); 
        }  
        public StatusService GetStatus()
        {
            try
            {
                Thread.Sleep(500);
                _serviceController.Refresh(); 
                return Enum.GetValues<StatusService>().ElementAt(((int)_serviceController.Status));
            }
            catch (InvalidOperationException)
            {
                return StatusService.NotCreate;
            }
        }
        public void DeleteService()
        {
            _serviceController.Refresh();
            if (_serviceController.Status != ServiceControllerStatus.Stopped)
            {
                StopService();
            } 
            Process.Start(new ProcessStartInfo
            {
                FileName = "sc",
                Arguments = $"delete \"{_serviceController.ServiceName}\"",
                Verb = "runas",
                CreateNoWindow = true,
                UseShellExecute = true
            }); 
            _serviceController = new ServiceController();
        }
    }
}
