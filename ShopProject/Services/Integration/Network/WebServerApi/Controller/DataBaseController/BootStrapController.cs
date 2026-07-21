using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.BootStrap;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.TaxObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class BootStrapController
    {
        private HttpClient _httpClient;

        public BootStrapController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<StartDataDto>> GetStartData()
        {  
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/BootStrap/Get");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();

            var result = ApiResponse<StartDataDto>.Unpacking(responseBody);

            return result;
        }
    }
}
