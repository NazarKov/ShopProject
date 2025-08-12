using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProjectSQLDataBase.Entities; 
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DataBaseController
{
    public class OperationRecorderAndUserController
    { 
        private HttpClient _httpClient;
        public OperationRecorderAndUserController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }  

        public async Task<bool> AddOperationRecordersAndUser(string token, List<OperationsRecorderUserEntity> item)
        { 
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorderAndUser/AddOperationRecordersAndUser?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result; 
        }

        public async Task<IEnumerable<OperationsRecorderUserEntity>> GetOperationRecordersAndUser(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorderAndUser/GetOperationRecordersAndUser?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<IEnumerable<OperationsRecorderUserEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (IEnumerable<OperationsRecorderUserEntity>)result;
        }

    }
}
