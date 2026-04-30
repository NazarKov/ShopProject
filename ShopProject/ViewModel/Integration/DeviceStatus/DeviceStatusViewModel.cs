using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.UI.Monitoring;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Monitoring.WebServerStatus.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace ShopProject.ViewModel.Integration.DeviceStatus
{
    internal class DeviceStatusViewModel : ViewModel<DeviceStatusViewModel>, IViewModelLoadResourse
    {
        private DispatcherTimer _timer;
        private IWebServerStatusService _webServerStatusService;
        private bool _isChecking;
        private bool isFristLoad;
        public DeviceStatusViewModel(IWebServerStatusService webServerStatusService)
        {
            _webServerStatusService = webServerStatusService;
            _timer = new DispatcherTimer();
            
            _deviceStatus = new ShopProject.Model.UI.Monitoring.DeviceStatus();
            isFristLoad = true;
        }

        private ShopProject.Model.UI.Monitoring.DeviceStatus _deviceStatus {  get; set; }
        public ShopProject.Model.UI.Monitoring.DeviceStatus DeviceStatus { get { return _deviceStatus; } set { _deviceStatus = value;OnPropertyChanged(nameof(DeviceStatus)); } }
        public Task LoadResourse()
        {
            SafeExecute(InitTimer);
            AddCommnadTimer();
            return Task.CompletedTask;
        }
        private void InitTimer()
        {
            _timer.Interval = TimeSpan.FromSeconds(3);//інтервал перевірки статусу девайсів

            _timer.Tick += async (s, e) =>
            {
                if (_isChecking) return;

                try
                {
                    _isChecking = true;

                    DeviceStatus = await CheckDeviseAvailable();
                    await CheckedDevises(DeviceStatus);
                }
                finally
                {
                    _isChecking = false;
                }
            }; 
        }
        private void AddCommnadTimer()
        {
            MediatorService.AddEventAsync("StartTimerCheckConnect",async () => { _timer.Start(); });
            MediatorService.AddEventAsync("StopTimerCheckConnect",async () => { _timer.Stop(); });
        }


        private async Task<ShopProject.Model.UI.Monitoring.DeviceStatus> CheckDeviseAvailable()
        {
            try
            {
                var result = new ShopProject.Model.UI.Monitoring.DeviceStatus();
                var availableWebServer = await _webServerStatusService.IsAvailableAsync();
                if (availableWebServer.IsEnabled)
                {
                    result.ServerStatusColor = Brushes.LightGreen; 
                }
                else
                {
                    result.ServerStatusColor = Brushes.IndianRed; 
                }
                if (availableWebServer.IsEnableDataBase)
                {
                    result.DataBaseStatusColor = Brushes.LightGreen; 

                }
                else
                {
                    result.DataBaseStatusColor = Brushes.IndianRed; 
                }
                return result;
            }
            catch
            { 
                return new ShopProject.Model.UI.Monitoring.DeviceStatus()
                {
                    DataBaseStatusColor = Brushes.IndianRed,
                    ServerStatusColor = Brushes.IndianRed,
                };

            }
        }
        private async Task CheckedDevises(ShopProject.Model.UI.Monitoring.DeviceStatus device)
        {
            if (device.DataBaseStatusColor == Brushes.LightGreen && device.ServerStatusColor == Brushes.LightGreen)
            {
                await MediatorService.ExecuteEventAsync("LostConnectSetHidden");
                if (isFristLoad)
                {
                    await MediatorService.ExecuteEventAsync("SetPageAfterLoadingResourse");
                    isFristLoad = false;
                }
            }
            else
            {
                await MediatorService.ExecuteEventAsync("LostConnectSetVisible");
            }
        } 
    }
}
