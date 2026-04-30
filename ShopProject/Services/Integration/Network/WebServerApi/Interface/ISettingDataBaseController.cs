using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.ControlWebServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Interface
{
    internal interface ISettingDataBaseController
    {
        public Task<string> Ping();

        public Task<ControlWebServerDto> IsAvailableServer();
    }
}
