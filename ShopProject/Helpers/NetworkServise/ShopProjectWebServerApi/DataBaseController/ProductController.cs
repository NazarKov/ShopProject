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
    public class ProductController
    {
        private string _url;
        public ProductController(string url)
        {
            _url = url;
        }

        public async Task<IEnumerable<ProductEntity>> GetProducts(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/Product/GetProducts?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<IEnumerable<ProductEntity>>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (IEnumerable<ProductEntity>)result;
            }
        }

        public async Task<ProductEntity> GetProductByBarCode(string token,string barCode)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/Product/GetProductsByBarCode?token={token}&barcode={barCode}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<ProductEntity>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (ProductEntity)result;
            }
        }


        public async Task<bool> AddProduct(string token, ProductEntity product)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var content = JsonSerializer.Serialize(product);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"/api/Product/AddProduct?token={token}", httpContent);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<bool>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (bool)result;
            }
        }

        public async Task<bool> AddProductRange(string token, List<ProductEntity> product)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var content = JsonSerializer.Serialize(product);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"/api/Product/AddProductRange?token={token}", httpContent);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<bool>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (bool)result;
            }
        }

        public async Task<bool> UpdateProduct(string token, ProductEntity product)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var content = JsonSerializer.Serialize(product);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"/api/Product/UpdateProduct?token={token}", httpContent);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<bool>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (bool)result;
            }
        }

        public async Task<bool> UpdateProductRange(string token, List<ProductEntity> product)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var content = JsonSerializer.Serialize(product);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"/api/Product/UpdateProductRange?token={token}", httpContent);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<bool>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (bool)result;
            }
        }

        public async Task<bool> UpdateParameterProduct(string token,string parameter , object value, ProductEntity product)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var content = JsonSerializer.Serialize(product);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"/api/Product/UpdateParameterProduct?token={token}&parameter={parameter}&value={value.ToString()}", httpContent);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<bool>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (bool)result;
            }
        }

    }
}
