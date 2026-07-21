using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.Product;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Product;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
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

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/Add", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();
            return ApiResponse<ProductDto>.Unpacking(responseBody);
        }

        public async Task<ApiResponse<bool>> Update(UpdateProductDto product)
        {
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/Update", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<bool>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result;
        }

        public async Task<ApiResponse<bool>> UpdateRange(IEnumerable<UpdateProductDto> product)
        {
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/UpdateRange", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<bool>> UpdateParameter(string parameter, object value, UpdateProductDto product)
        {
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/UpdateParameter?parameter={parameter}&value={value.ToString()}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<PaginatorDto<ProductDto, int>>> GetPageColumn(PaginatorDto<ProductDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<ProductDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/GetPageColumn", httpContent);

            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();

            var result = ApiResponse<PaginatorDto<ProductDto, int>>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<PaginatorDto<ProductDto, int>>> GetByNamePageColumn(string name, PaginatorDto<ProductDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<ProductDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/GetByNamePageColumn?name={name}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorDto<ProductDto, int>>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<PaginatorDto<ProductDto, int>>> GetProductsByBarCode(string barCode, PaginatorDto<ProductDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<ProductDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/GetByBarCodePageColumn?barCode={barCode}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<PaginatorDto<ProductDto, int>>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result;
        } 
        public async Task<ApiResponse<ProductDto>> GetProductByBarCode(string barCode)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetByBarCode?barCode={barCode}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<ProductDto>.Unpacking(responseBody);

            return result;
        }   
        public async Task<ProductInfoDto> GetProductInfo(string token)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetInfoProducts?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<ProductInfoDto>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

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

        
        //public async Task<ProductDto> GetProductByBarCode(string token, string barCode, TypeStatusProduct statusProduct)
        //{
        //    HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetProductsByBarCode?token={token}&barcode={barCode}&status={statusProduct}");
        //    string responseBody = await httpResponse.Content.ReadAsStringAsync();

        //    httpResponse.EnsureSuccessStatusCode();
        //    var result = ApiResponse<ProductDto>.Unpacking(responseBody);

        //    return result.Data;
        //}

        //public async Task<Paginator<ProductDto>> GetProductsByBarCode(string token, string barCode, int page, int countColumn, TypeStatusProduct statusProduct)
        //{
        //    HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/Product/GetAllProductsByBarCode?token={token}&page={page}&countColumn={countColumn}&barcode={barCode}&status={statusProduct}");
        //    string responseBody = await httpResponse.Content.ReadAsStringAsync();

        //    httpResponse.EnsureSuccessStatusCode();
        //    var result = ApiResponse<Paginator<ProductDto>>.Unpacking(responseBody);

        //    return result.Data;
        //}
         

        //public async Task<bool> AddProductRange(string token, IEnumerable<Product> product)
        //{ 
        //    var content = JsonSerializer.Serialize(product.ToCreateProductDto());
        //    HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

        //    HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/Product/AddProductRange?token={token}", httpContent);
        //    string responseBody = await httpResponse.Content.ReadAsStringAsync();

        //    httpResponse.EnsureSuccessStatusCode();
        //    var result = ApiResponse<bool>.Unpacking(responseBody);

        //    return result.Data; 
        //}
         
    }
}
