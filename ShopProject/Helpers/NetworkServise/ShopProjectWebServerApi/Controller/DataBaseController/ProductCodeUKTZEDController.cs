using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.ProductCodeUKTZED; 
using ShopProject.Helpers.Template.Paginator;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class ProductCodeUKTZEDController
    { 
        private HttpClient _httpClient;
        public ProductCodeUKTZEDController(string url)
        {

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<bool> AddProductCodeUKTZED(string token, CreateProductUKTZEDDto codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/AddCodeUKTZED?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<bool> UpdateCodeUKTZED(string token, UpdateProductCodeUKTZEDDto codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/UpdateCodeUKTZED?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<bool> UpdateParameterCodeUKTZED(string token, string parameter, object value, UpdateProductCodeUKTZEDDto codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/UpdateParameterCodeUKTZED?token={token}&parameter={parameter}&value={value.ToString()}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<bool> DeleteCodeUKTZED(string token, ProductCodeUKTZED codeUKTZED)
        {
            var content = string.Empty;
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/DeleteCodeUKTZEDE?token={token}&id={codeUKTZED.ID}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<ProductCodeUKTZEDDto> GetCodeUKTZEDEByCode(string token, string code, TypeStatusCodeUKTZED status)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductCodeUKTZED/GetCodeUKTZEDEByCode?token={token}&code={code}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<ProductCodeUKTZEDDto>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<PaginatorData<ProductCodeUKTZEDDto>> GetCodeUKTZEDByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductCodeUKTZED/GetCodeUKTZEDByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<ProductCodeUKTZEDDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<PaginatorData<ProductCodeUKTZEDDto>> GetCodeUKTZEDPageColumn(string token, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductCodeUKTZED/GetCodeUKTZEDPageColumn?token={token}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<ProductCodeUKTZEDDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<IEnumerable<ProductCodeUKTZEDDto>> GetCodeUKTZED(string token)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductCodeUKTZED/GetCodeUKTZED?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<ProductCodeUKTZEDDto>>.Unpacking(responseBody);

            return result.Data;
        }
    }
}
