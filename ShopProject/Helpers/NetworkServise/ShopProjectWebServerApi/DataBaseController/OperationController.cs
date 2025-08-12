using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper; 
using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DataBaseController
{
    public class OperationController
    {
        private string _url;
        public OperationController(string url)
        {
            _url = url;
        }
        public async Task<OperationEntity> GetLastOperation(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/Operation/GetLastOperation?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<OperationEntity>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (OperationEntity)result;
            }
        }

        public async Task<IEnumerable<OperationEntity>> GetOperations(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/Operation/GetOperations?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<IEnumerable<OperationEntity>>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (IEnumerable<OperationEntity>)result;
            }
        }

        public async Task<bool> AddOperation(string token, OperationEntity item)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var content = JsonSerializer.Serialize(item);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"/api/Operation/AddOperation?token={token}", httpContent);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<bool>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (bool)result;
            }
        }
    }
}
