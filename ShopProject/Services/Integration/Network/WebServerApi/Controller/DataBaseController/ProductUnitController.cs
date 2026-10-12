using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ProductUnit;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator; 
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class ProductUnitController
    {
        private readonly HttpClient _httpClient;
        public ProductUnitController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<ProductUnitDto>> Add(CreateProductUnitDto unit)
        {
            var content = JsonSerializer.Serialize(unit);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            return await SendHttpRequest.SendAsync<ProductUnitDto>(() => _httpClient.PostAsync($"/api/ProductUnit/Add", httpContent)); 
        }

        public async Task<ApiResponse<bool>> Update(UpdateProductUnitDto unit)
        {
            var content = JsonSerializer.Serialize(unit);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/ProductUnit/Update", httpContent)); 
        }

        public async Task<ApiResponse<bool>> UpdateParameter(string parameter, object value, UpdateProductUnitDto product)
        {
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/ProductUnit/UpdateParameter?parameter={parameter}&value={value.ToString()}", httpContent)); 
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var content = JsonSerializer.Serialize(id.ToString());
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/ProductUnit/Delete", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<ProductUnitDto,int>>> GetUnitByCode(string code, PaginatorDto<ProductUnitDto, int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<ProductUnitDto, int>>(() => _httpClient.PostAsync($"/api/ProductUnit/GetByCodePageColumn?code={code}", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<ProductUnitDto, int>>> GetByNamePageColumn(string name, PaginatorDto<ProductUnitDto, int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<ProductUnitDto, int>>(() => _httpClient.PostAsync($"/api/ProductUnit/GetByNamePageColumn?name={name}", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<ProductUnitDto, int>>> GetPageColumn(PaginatorDto<ProductUnitDto, int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<ProductUnitDto, int>>(() => _httpClient.PostAsync($"/api/ProductUnit/GetPageColumn", httpContent)); 
        }

        public async Task<ApiResponse<IEnumerable<ProductUnitDto>>> GetAll()
        {
            return await SendHttpRequest.SendAsync<IEnumerable<ProductUnitDto>>(() => _httpClient.GetAsync($"/api/ProductUnit/GetAll")); 
        }
    }
}
