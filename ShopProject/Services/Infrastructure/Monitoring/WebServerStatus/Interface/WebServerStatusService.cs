using ShopProject.Model.Integration.Monitoring.WebServer;
using ShopProject.Services.Modules.Common;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.Monitoring.WebServerStatus.Interface
{
    internal interface IWebServerStatusService
    {
        public Task<OperationResult<ControlWebServer>> IsAvailableAsync();

        public Task<bool> HasInternetAsync();
    }
}
