using Azure;
using ShopProject.Model.Integration.Monitoring.WebServer;
using ShopProject.Services.Infrastructure.Monitoring.WebServerStatus.Interface;
using ShopProject.Services.Integration.Network.Network.Interface;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.ControlWebServer;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Mapping.ControlWebServer;
using ShopProject.Services.Modules.Mapping.OperaionResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.Monitoring.WebServerStatus
{
    internal class WebServerStatusService : IWebServerStatusService
    {
        private IMainWebServerService _webServerService;
        private INetworkService _networkService;
        public WebServerStatusService(IMainWebServerService mainWebServerService, INetworkService networkService)
        {
            _webServerService = mainWebServerService;
            _networkService = networkService;
        }

        public async Task<bool> HasInternetAsync() => await _networkService.HasInternetAsync(); 

        public async Task<OperationResult<ControlWebServer>> IsAvailableAsync()
        {
            try
            {
                var response = (await _webServerService.Settings.IsAvailableServer()).ToOperationResult();
                if (response.IsSuccess)
                {
                    return OperationResult<ControlWebServer>.Success(response.Data.ToControlWebServer());
                }
                else
                {
                    return OperationResult<ControlWebServer>.Fail(response.ErrorMessage, response.ErrorType);
                }
            }
            catch
            {
                return OperationResult<ControlWebServer>.Fail("Невдлося підключитися до сервера");
            } 
        }
    }
}
