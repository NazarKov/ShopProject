using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.ControlWebServer; 
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Interface
{
    internal interface ISettingDataBaseController
    {
        public Task<string> Ping();

        public Task<ApiResponse<ControlWebServerDto>> IsAvailableServer();
    }
}
