using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Product;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator; 
using System.Collections.Generic; 
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class ProductController
    { 
        private readonly HttpClient _httpClient;
        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient; 

        }

        public async Task<ApiResponse<ProductDto>> Add(CreateProductDto product)
        {
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<ProductDto>(() => _httpClient.PostAsync($"/api/Product/Add", httpContent)); 
        }

        public async Task<ApiResponse<bool>> Update(UpdateProductDto product)
        {
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/Product/Update", httpContent)); 
        }

        public async Task<ApiResponse<bool>> UpdateRange(IEnumerable<UpdateProductDto> product)
        {
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/Product/UpdateRange", httpContent)); 
        }

        public async Task<ApiResponse<bool>> UpdateParameter(string parameter, object value, UpdateProductDto product)
        {
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/Product/UpdateParameter?parameter={parameter}&value={value.ToString()}", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<ProductDto, int>>> GetPageColumn(PaginatorDto<ProductDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<ProductDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            return await SendHttpRequest.SendAsync<PaginatorDto<ProductDto, int>>(() => _httpClient.PostAsync($"/api/Product/GetPageColumn", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<ProductDto, int>>> GetByNamePageColumn(string name, PaginatorDto<ProductDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<ProductDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<ProductDto, int>>(() => _httpClient.PostAsync($"/api/Product/GetByNamePageColumn?name={name}", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<ProductDto, int>>> GetProductsByBarCode(string barCode, PaginatorDto<ProductDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<ProductDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<ProductDto, int>>(() => _httpClient.PostAsync($"/api/Product/GetByBarCodePageColumn?barCode={barCode}", httpContent)); 
        } 
        public async Task<ApiResponse<ProductDto>> GetProductByBarCode(string barCode)
        {
            return await SendHttpRequest.SendAsync<ProductDto>(() => _httpClient.GetAsync($"/api/Product/GetByBarCode?barCode={barCode}"));
        }   
        public async Task<ApiResponse<ProductInfoDto>> GetProductInfo()
        {
            return await SendHttpRequest.SendAsync<ProductInfoDto>(() => _httpClient.GetAsync($"/api/Product/GetInfoProducts")); 
        } 

        public async Task<ApiResponse<IEnumerable<ProductDto>>> GetProducts(string token)
        {
            return await SendHttpRequest.SendAsync<IEnumerable<ProductDto>>(() => _httpClient.GetAsync($"/api/Product/GetProducts?token={token}")); 
        } 
    }
}
