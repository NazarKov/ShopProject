using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.ControlWebServer;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller
{
    public class SettingsController : ISettingDataBaseController
    {
        private HttpClient _httpClient;  
        public SettingsController(HttpClient httpClient)
        {
            _httpClient = httpClient;  
        }

        public async Task<ApiResponse<ControlWebServerDto>> IsAvailableServer()
        {
            return await SendHttpRequest.SendAsync<ControlWebServerDto>(() => _httpClient.GetAsync("/api/Settings/Health")); 
        }

        public async Task<ApiResponse<string>> Ping()
        {
            return await SendHttpRequest.SendAsync<string>(() => _httpClient.GetAsync("/api/Settings/Ping")); 
        }
    }
}
