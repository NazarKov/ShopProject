using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ProductCodeUKTZED;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator;  
using System.Collections.Generic; 
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class ProductCodeUKTZEDController
    { 
        private readonly HttpClient _httpClient;
        public ProductCodeUKTZEDController(HttpClient httpClient)
        { 
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<ProductCodeUKTZEDDto>> Add(CreateProductUKTZEDDto codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<ProductCodeUKTZEDDto>(() => _httpClient.PostAsync($"/api/ProductCodeUKTZED/Add", httpContent)); 
        }

        public async Task<ApiResponse<bool>> Update(UpdateProductCodeUKTZEDDto codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/ProductCodeUKTZED/Update", httpContent)); 
        }

        public async Task<ApiResponse<bool>> UpdateParameter(string parameter, object value, UpdateProductCodeUKTZEDDto codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/ProductCodeUKTZED/UpdateParameter?&parameter={parameter}&value={value.ToString()}", httpContent)); 
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        { 
            HttpContent httpContent = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/ProductCodeUKTZED/Delete", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<ProductCodeUKTZEDDto,int>>> GetByCode(string code, PaginatorDto<ProductCodeUKTZEDDto, int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<ProductCodeUKTZEDDto, int>> (() => _httpClient.PostAsync($"/api/ProductCodeUKTZED/GetByCode?code={code}", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<ProductCodeUKTZEDDto,int>>> GetByNamePageColumn(string name, PaginatorDto<ProductCodeUKTZEDDto, int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<ProductCodeUKTZEDDto, int>>(() => _httpClient.PostAsync($"/api/ProductCodeUKTZED/GetByNamePageColumn?name={name}", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<ProductCodeUKTZEDDto,int>>> GetPageColumn(PaginatorDto<ProductCodeUKTZEDDto,int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<ProductCodeUKTZEDDto, int>> (() => _httpClient.PostAsync($"/api/ProductCodeUKTZED/GetPageColumn", httpContent)); 
        }

        public async Task<ApiResponse<IEnumerable<ProductCodeUKTZEDDto>>> GetAll()
        {
            return await SendHttpRequest.SendAsync<IEnumerable<ProductCodeUKTZEDDto>>(() => _httpClient.GetAsync($"/api/ProductCodeUKTZED/GetAll")); 
        }
    }
}
