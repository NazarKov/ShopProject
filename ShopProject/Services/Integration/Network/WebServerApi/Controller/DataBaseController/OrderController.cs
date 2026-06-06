using ShopProject.Model.Domain.Order;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Modules.Mapping.Order; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class OrderController
    {
        private HttpClient _httpClient;
        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> AddOrderRange(string token, List<Order> orders)
        {
            var content = JsonSerializer.Serialize(orders.ToListCreatOrderDto());
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Order/AddOrderRange?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<bool>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data;
        }

        //public async Task<IEnumerable<OrderEntity>> GetOrders(string token)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(_url);

        //        HttpResponseMessage httpResponse = await client.GetAsync($"/api/Order/GetOrders?token={token}");
        //        string responseBody = await httpResponse.Content.ReadAsStringAsync();

        //        var result = ApiResponse<IEnumerable<OrderEntity>>.Unpacking(responseBody);
        //        httpResponse.EnsureSuccessStatusCode();

        //        return result.Data;
        //    }
        //}
    }
}
