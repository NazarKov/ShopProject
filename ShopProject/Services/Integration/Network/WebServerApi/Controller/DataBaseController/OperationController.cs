using ShopProject.Model.Domain.Operation;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Operation;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Modules.Mapping.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class OperationController
    { 
        private HttpClient _httpClient;
        public OperationController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<OperationDto>> Add(CreateOperationDto item)
        {
            var operation = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(operation, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Operation/Add", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<OperationDto>.Unpacking(responseBody);

            return result;
        }


        public async Task<ApiResponse<OperationІnformationDto>> GetLastNumberOperation(int shiftId)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Operation/GetLastNumberOperation?shiftId={shiftId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<OperationІnformationDto>.Unpacking(responseBody);

            return result;
        }
        public async Task<ApiResponse<OperationInfoDto>> GetOperationsInfo(int shiftId)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Operation/GetOperationsInfo?shiftId={shiftId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<OperationInfoDto>.Unpacking(responseBody);

            return result;
        }   
      
    }
}
