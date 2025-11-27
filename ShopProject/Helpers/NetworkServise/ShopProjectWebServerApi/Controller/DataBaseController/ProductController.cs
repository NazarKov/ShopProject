using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Product;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class ProductController
    { 
        private readonly HttpClient _httpClient;
        public ProductController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);

        }
        public async Task<ProductInfoDto> GetProductInfo(string token)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetInfoProducts?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<ProductInfoDto>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data;
        }

        public async Task<ProductEntity> GetProductsByBarCode(string token, string barCode, TypeStatusProduct statusProduct)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetProductsByBarCode?token={token}&barCode={barCode}&status={statusProduct}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
             
            var result = ApiResponse<ProductEntity>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data;
        }

        public async Task<PaginatorData<ProductDto>> GetProductByNamePageColumn(string token,string name, int page, int countColumn, TypeStatusProduct statusProduct)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetProductByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={statusProduct}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<ProductDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<PaginatorData<ProductDto>> GetProductsPageColumn(string token, int page, int countColumn , TypeStatusProduct statusProduct)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetProductsPageColumn?token={token}&countColumn={countColumn}&page={page}&status={statusProduct}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<ProductDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(string token)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetProducts?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<ProductDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<ProductDto> GetProductByBarCode(string token, string barCode)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetProductsByBarCode?token={token}&barCode={barCode}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<ProductDto>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<ProductDto> GetProductByBarCode(string token,string barCode, TypeStatusProduct statusProduct)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetProductsByBarCode?token={token}&barcode={barCode}&status={statusProduct}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<ProductDto>.Unpacking(responseBody);

            return result.Data; 
        }


        public async Task<bool> AddProduct(string token, CreateProductDto product)
        { 
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/AddProduct?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data; 
        }

        public async Task<bool> AddProductRange(string token, IEnumerable<Product> product)
        { 
            var content = JsonSerializer.Serialize(product.ToCreateProductDto());
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/AddProductRange?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data; 
        }

        public async Task<bool> UpdateProduct(string token, UpdateProductDto product)
        { 
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/UpdateProduct?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<bool>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data; 
        }

        public async Task<bool> UpdateProductRange(string token, IEnumerable<UpdateProductDto> product)
        { 
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/UpdateProductRange?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<bool> UpdateParameterProduct(string token,string parameter , object value, UpdateProductDto product)
        { 
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/UpdateParameterProduct?token={token}&parameter={parameter}&value={value.ToString()}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data; 
        }

    }
}
