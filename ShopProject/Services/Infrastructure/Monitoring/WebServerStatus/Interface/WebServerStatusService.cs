using ShopProject.Model.Integration.Monitoring.WebServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.Monitoring.WebServerStatus.Interface
{
    internal interface IWebServerStatusService
    {
        public Task<ControlWebServer> IsAvailableAsync();
    }
}
