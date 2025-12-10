using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.StoragePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class DiscountController
    {
        private HttpClient _httpClient;

        public DiscountController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<int> AddDiscount(string token, Discount item)
        {
            var discount = JsonSerializer.Serialize(item.ToCreateDicount());
            HttpContent httpContent = new StringContent(discount, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Discount/AddDiscount?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<int>.Unpacking(responseBody);

            return result.Data;
        }
    }
}
