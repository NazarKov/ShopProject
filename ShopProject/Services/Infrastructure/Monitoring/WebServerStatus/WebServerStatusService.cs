using ShopProject.Model.Integration.Monitoring.WebServer;
using ShopProject.Services.Infrastructure.Monitoring.WebServerStatus.Interface;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.ControlWebServer;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Integration.Network.WebServerApi.Mapping;
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
        public WebServerStatusService(IMainWebServerService mainWebServerService)
        {
            _webServerService = mainWebServerService;
        }

        public async Task<ControlWebServer> IsAvailableAsync()
        {
            return (await _webServerService.Settings.IsAvailableServer()).ToControlWebServer();
        }
    }
}
