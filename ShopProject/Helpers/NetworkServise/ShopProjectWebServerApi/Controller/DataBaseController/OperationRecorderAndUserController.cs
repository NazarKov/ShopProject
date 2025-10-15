using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.OperationRecorderUser;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
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
    public class OperationRecorderAndUserController
    { 
        private HttpClient _httpClient;
        public OperationRecorderAndUserController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }  

        public async Task<bool> AddOperationRecordersAndUser(string token, Guid userId ,List<BindingUserToOperationRecorderDto> items)
        {
            var operationsRecorderUserEntity = JsonSerializer.Serialize(items);
            HttpContent httpContent = new StringContent(operationsRecorderUserEntity, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorderAndUser/AddOperationRecordersAndUser?token={token}&userId={userId}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data; 
        }

        public async Task<OperationRecorderUserDto> GetOperationRecordersAndUser(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorderAndUser/GetOperationRecordersAndUser?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<OperationRecorderUserDto>.Unpacking(responseBody);

            return result.Data;
        }

    }
}
