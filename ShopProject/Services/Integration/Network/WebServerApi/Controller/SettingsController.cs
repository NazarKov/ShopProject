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
        public SettingsController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.Timeout = TimeSpan.FromSeconds(4);
        }

        public async Task<ControlWebServerDto> IsAvailableServer()
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("/api/Settings/Health");
            string responseBody = await responseMessage.Content.ReadAsStringAsync(); 
            var result = ApiResponse<ControlWebServerDto>.Unpacking(responseBody); 
            return result.Data;
        }

        public async Task<string> Ping()
        { 
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("/api/Settings/Ping");
            string responseBody = await responseMessage.Content.ReadAsStringAsync();
            responseMessage.EnsureSuccessStatusCode();

            var result = ApiResponse<string>.Unpacking(responseBody);
            
            return result.Data.ToString(); 
        }
    }
}
