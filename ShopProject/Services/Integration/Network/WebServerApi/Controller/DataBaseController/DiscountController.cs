using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Discount;
using ShopProject.Services.Integration.Network.WebServerApi.Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class DiscountController
    {
        private HttpClient _httpClient;

        public DiscountController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<int> AddDiscount(string token, CreateDiscountDto item)
        {
            var discount = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(discount, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Discount/AddDiscount?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<int>.Unpacking(responseBody);

            return result.Data;
        }
    }
}
