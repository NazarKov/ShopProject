using ShopProject.Model.Domain.Order;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Modules.Mapping.Order;  
using System.Collections.Generic; 
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class OrderController
    {
        private readonly HttpClient _httpClient;
        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<bool>> AddOrderRange(List<Order> orders)
        {
            var content = JsonSerializer.Serialize(orders.ToListCreatOrderDto());
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/Order/AddRange", httpContent)); 
        } 
    }
}
