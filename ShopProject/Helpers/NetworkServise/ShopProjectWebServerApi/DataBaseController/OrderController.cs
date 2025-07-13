using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DataBaseController
{
    public class OrderController
    {
        private string _url;
        public OrderController(string url)
        {
            _url = url;
        }
        public async Task<bool> AddOrderRange(string token, List<OrderEntity> product)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var content = JsonSerializer.Serialize(product);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"/api/Order/AddOrderRange?token={token}", httpContent);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<bool>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (bool)result;
            }
        }

        public async Task<IEnumerable<OrderEntity>> GetOrders(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/Order/GetOrders?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<IEnumerable<OrderEntity>>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (IEnumerable<OrderEntity>)result;
            }
        }
    }
}
