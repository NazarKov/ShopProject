using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller
{
    public class SettingsController
    {
        private HttpClient _httpClient;

        public SettingsController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.Timeout = TimeSpan.FromSeconds(2);
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
