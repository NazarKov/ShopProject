using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel;
using ShopProjectSQLDataBase.Entities;
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
        public async Task<OperationEntity> GetLastOperation(string token,int shiftId)
        {   
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Operation/GetLastOperation?token={token}&shiftId={shiftId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<OperationEntity>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (OperationEntity)result; 
        }

        public async Task<IEnumerable<OperationEntity>> GetOperations(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Operation/GetOperations?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<IEnumerable<OperationEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (IEnumerable<OperationEntity>)result; 
        }

        public async Task<bool> AddOperation(string token, UIOperationModel item)
        { 
            var operation = JsonSerializer.Serialize(item.ToCreateOperationDto());
            HttpContent httpContent = new StringContent(operation, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Operation/AddOperation?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result; 
        }
    }
}
