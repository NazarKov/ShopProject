using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Discount;
using ShopProject.Services.Integration.Network.WebServerApi.Common;  
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class DiscountController
    {
        private readonly HttpClient _httpClient;

        public DiscountController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<int>> AddDiscount(CreateDiscountDto item)
        { 
            var discount = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(discount, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<int>(()=> _httpClient.PostAsync($"/api/Discount/Add", httpContent)); 
        }
    }
}
