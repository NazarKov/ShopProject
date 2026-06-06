using ShopProject.Model.Integration.Monitoring.WebServer;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.ControlWebServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.ControlWebServer
{
    internal static class ApiControlWebServerMappingExtensions
    {
        public static ShopProject.Model.Integration.Monitoring.WebServer.ControlWebServer ToControlWebServer(this ControlWebServerDto item)
        {
            return new ShopProject.Model.Integration.Monitoring.WebServer.ControlWebServer()
            {
                IsEnabled = item.IsEnabled,
                IsEnableDataBase = item.IsEnableDataBase,
            };
        }
    }
}
