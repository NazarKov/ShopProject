using ShopProject.Services.Infrastructure.Monitoring.WebServerStatus.Interface;
using ShopProject.Services.Modules.Main.MainComponent.DeviceStatus.Interface; 
using System.Threading.Tasks; 
using DeviseStatusModel = ShopProject.Model.Domain.Monitoring.DeviceStatus;

namespace ShopProject.Services.Modules.Main.MainComponent.DeviceStatus
{
    internal class DeviceStatusService : IDeviceStatusService
    {
        private readonly IWebServerStatusService _webServerStatusService; 
        private bool _isChecking; 
        private DeviseStatusModel _deviseStatusModel;
        public DeviceStatusService(IWebServerStatusService webServerStatusService)
        {
            _webServerStatusService = webServerStatusService; 
            _deviseStatusModel = new DeviseStatusModel();
        }

        public async Task Check()
        {
            if (_isChecking) return;

            try
            {
                _isChecking = true;

                _deviseStatusModel = await CheckDeviseAvailable();
            }
            finally
            {
                _isChecking = false;
            }
        } 

        public DeviseStatusModel GetStatus()
        {
            return _deviseStatusModel;
        }

        private async Task<DeviseStatusModel> CheckDeviseAvailable()
        {
            try
            {
                var result = new DeviseStatusModel();
                var availableInternet = await _webServerStatusService.HasInternetAsync();
                if (availableInternet)
                {
                    result.InternetStatus = true;

                    var availableWebServer = await _webServerStatusService.IsAvailableAsync();
                    if (availableWebServer.IsSuccess)
                    {
                        if (availableWebServer.Data != null)
                        {
                            if (availableWebServer.Data.IsEnabled)
                            {
                                result.ServerStatus = true;
                            }
                            else
                            {
                                result.ServerStatus = false;
                            }
                            if (availableWebServer.Data.IsEnableDataBase)
                            {
                                result.DataBaseStatus = true;

                            }
                            else
                            {
                                result.DataBaseStatus = false;
                            }
                        } 
                    }
                    else
                    {
                        result.ServerStatus = false; ;
                        result.DataBaseStatus = false;
                    }
                }
                else
                {
                    result.InternetStatus = false;
                    result.ServerStatus = false;
                    result.DataBaseStatus = false;
                }
                return result;
            }
            catch
            {
                return new DeviseStatusModel()
                {
                    DataBaseStatus = false,
                    ServerStatus = false, 
                };

            }
        } 
    }
}
