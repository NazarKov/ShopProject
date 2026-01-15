using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Operation;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.SalePage;
using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class OperationController
    { 
        private HttpClient _httpClient;
        public OperationController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }
        public async Task<OperationInfoDto> GetOperationsInfo(string token, int shiftId)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Operation/GetOperationsInfo?token={token}&shiftId={shiftId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<OperationInfoDto>.Unpacking(responseBody);

            return result.Data;
        } 
        public async Task<string> GetLastNumberOperation(string token,int shiftId)
        {   
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Operation/GetLastNumberOperation?token={token}&shiftId={shiftId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<string>.Unpacking(responseBody);

            return result.Data; 
        }

        public async Task<IEnumerable<OperationEntity>> GetOperations(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Operation/GetOperations?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<OperationEntity>>.Unpacking(responseBody);

            return result.Data; 
        }

        public async Task<int> AddOperation(string token, Operation item)
        { 
            var operation = JsonSerializer.Serialize(item.ToCreateOperationDto());
            HttpContent httpContent = new StringContent(operation, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Operation/AddOperation?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<int>.Unpacking(responseBody);

            return result.Data; 
        }
    }
}
