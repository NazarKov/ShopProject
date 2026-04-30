using ShopProject.Model.Integration.Monitoring.WebServer;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.ControlWebServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Mapping
{
    internal static class ControlWebServerMappingExtensions
    {
        public static ControlWebServer ToControlWebServer(this ControlWebServerDto item)
        {
            return new ControlWebServer()
            {
                IsEnabled = item.IsEnabled,
                IsEnableDataBase = item.IsEnableDataBase,
            };
        }
    }
}
