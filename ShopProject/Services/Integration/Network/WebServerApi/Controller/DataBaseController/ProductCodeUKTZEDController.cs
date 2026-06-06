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
        private HttpClient _httpClient;
        public ProductCodeUKTZEDController(HttpClient httpClient)
        {

            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<ProductCodeUKTZEDDto>> Add(CreateProductUKTZEDDto codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/Add", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<ProductCodeUKTZEDDto>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<bool>> Update(UpdateProductCodeUKTZEDDto codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/Update", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<bool>> UpdateParameter(string parameter, object value, UpdateProductCodeUKTZEDDto codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/UpdateParameter?&parameter={parameter}&value={value.ToString()}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var content = id;
            HttpContent httpContent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/Delete", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<PaginatorDto<ProductCodeUKTZEDDto,int>>> GetByCode(string code, PaginatorDto<ProductCodeUKTZEDDto, int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/GetByCode?code={code}",httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorDto<ProductCodeUKTZEDDto, int>>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<PaginatorDto<ProductCodeUKTZEDDto,int>>> GetByNamePageColumn(string name, PaginatorDto<ProductCodeUKTZEDDto, int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/GetByNamePageColumn?name={name}",httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorDto<ProductCodeUKTZEDDto,int>>.Unpacking(responseBody); 
            return result;
        }

        public async Task<ApiResponse<PaginatorDto<ProductCodeUKTZEDDto,int>>> GetPageColumn(PaginatorDto<ProductCodeUKTZEDDto,int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/GetPageColumn", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorDto<ProductCodeUKTZEDDto, int>>.Unpacking(responseBody); 
            return result;
        }

        public async Task<ApiResponse<IEnumerable<ProductCodeUKTZEDDto>>> GetAll()
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductCodeUKTZED/GetAll");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<ProductCodeUKTZEDDto>>.Unpacking(responseBody);

            return result;
        }
    }
}
