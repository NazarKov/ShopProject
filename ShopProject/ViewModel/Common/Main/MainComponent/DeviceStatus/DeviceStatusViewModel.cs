using ShopProject.Infrastructure.Mvvm;
using ShopProject.Infrastructure.Mvvm.Interface;
using ShopProject.Services.Infrastructure.Mediator; 
using ShopProject.Services.Modules.Main.MainComponent.DeviceStatus.Interface;
using System;
using System.Threading;
using System.Threading.Tasks; 
using System.Windows.Threading;

namespace ShopProject.ViewModel.Common.Main.MainComponent.DeviceStatus
{
    internal class DeviceStatusViewModel : ViewModel<DeviceStatusViewModel>, IViewModelLoadResourse
    {  
        private readonly IDeviceStatusService _deviceStatusService;
        private readonly SemaphoreSlim _gate = new(1, 1);
        private readonly DispatcherTimer _timer;
        private (bool Server, bool Db, bool Net)? _lastStatus;
        private bool _isFirstLoad;
        private Model.UI.Monitoring.DeviceStatus _deviceStatusOld;
        public DeviceStatusViewModel(IDeviceStatusService deviceStatusService)
        { 
            _deviceStatusService = deviceStatusService;
            _deviceStatus = new Model.UI.Monitoring.DeviceStatus();
            _deviceStatusOld = new Model.UI.Monitoring.DeviceStatus();
            _timer = new DispatcherTimer();
            _isFirstLoad = true;
        }

        private Model.UI.Monitoring.DeviceStatus _deviceStatus {  get; set; }
        public Model.UI.Monitoring.DeviceStatus DeviceStatus { get { return _deviceStatus; } set { _deviceStatus = value;OnPropertyChanged(nameof(DeviceStatus)); } }
        public Task LoadResourse()
        {
            _timer.Interval = TimeSpan.FromSeconds(3);
            SafeExecute(() =>
            {
                _timer.Tick += async (e,x) => { await _deviceStatusService.Check(); await ChangeStatus(); }; 
            });
            AddCommnadTimer();
            return Task.CompletedTask;
        }
       
        private void AddCommnadTimer()
        {
            MediatorService.AddEventAsync("StartTimerCheckConnect",async () => { _timer.Start(); });
            MediatorService.AddEventAsync("StopTimerCheckConnect",async () => { _timer.Stop(); _isFirstLoad = true; });
        }
        private async Task ChangeStatus()
        {
            if (!await _gate.WaitAsync(0)) return; 
            try
            {
                var s = _deviceStatusService.GetStatus();
                var current = (s.ServerStatus, s.DataBaseStatus, s.InternetStatus);

                if (_lastStatus == current)
                    return; 

                _lastStatus = current;
                DeviceStatus.ChangeStatus(current.ServerStatus, current.DataBaseStatus, current.InternetStatus);

                bool allAvailable = current.ServerStatus && current.DataBaseStatus && current.InternetStatus;

                if (allAvailable)
                {
                    await MediatorService.ExecuteEventAsync("LostConnectSetHidden");

                    if (_isFirstLoad)
                    {
                        _isFirstLoad = false;
                        await MediatorService.ExecuteEventAsync("SetPageAfterLoadingResourse");
                    }
                }
                else
                {
                    await MediatorService.ExecuteEventAsync("LostConnectSetVisible");
                    MediatorService.ExecuteEvent("CloseSecondaryWindows");
                }
            } 
            finally { _gate.Release(); }
        } 
    }
}
