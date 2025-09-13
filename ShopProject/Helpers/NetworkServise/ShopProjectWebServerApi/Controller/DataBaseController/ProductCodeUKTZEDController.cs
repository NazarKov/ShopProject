using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProject.Helpers.Template.Paginator;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
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

        public async Task<bool> AddProductCodeUKTZED(string token, ProductCodeUKTZEDEntity codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/AddCodeUKTZED?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = CheckingResponse.Unpacking<bool>(responseBody);

            return (bool)result;
        }

        public async Task<bool> UpdateCodeUKTZED(string token, ProductCodeUKTZEDEntity codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/UpdateCodeUKTZED?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = CheckingResponse.Unpacking<bool>(responseBody);

            return (bool)result;
        }

        public async Task<bool> UpdateParameterCodeUKTZED(string token, string parameter, object value, ProductCodeUKTZEDEntity codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/UpdateParameterCodeUKTZED?token={token}&parameter={parameter}&value={value.ToString()}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = CheckingResponse.Unpacking<bool>(responseBody);

            return (bool)result;
        }

        public async Task<bool> DeleteCodeUKTZED(string token, ProductCodeUKTZEDEntity codeUKTZED)
        {
            var content = JsonSerializer.Serialize(codeUKTZED);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductCodeUKTZED/DeleteCodeUKTZEDE?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = CheckingResponse.Unpacking<bool>(responseBody);

            return (bool)result;
        }

        public async Task<ProductCodeUKTZEDEntity> GetCodeUKTZEDEByCode(string token, string code, TypeStatusCodeUKTZED status)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductCodeUKTZED/GetCodeUKTZEDEByCode?token={token}&code={code}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<ProductCodeUKTZEDEntity>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (ProductCodeUKTZEDEntity)result;
        }

        public async Task<PaginatorData<ProductCodeUKTZEDEntity>> GetCodeUKTZEDByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductCodeUKTZED/GetCodeUKTZEDByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<PaginatorData<ProductCodeUKTZEDEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (PaginatorData<ProductCodeUKTZEDEntity>)result;
        }

        public async Task<PaginatorData<ProductCodeUKTZEDEntity>> GetCodeUKTZEDPageColumn(string token, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductCodeUKTZED/GetCodeUKTZEDPageColumn?token={token}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<PaginatorData<ProductCodeUKTZEDEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (PaginatorData<ProductCodeUKTZEDEntity>)result;
        }

        public async Task<IEnumerable<ProductCodeUKTZEDEntity>> GetCodeUKTZED(string token)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductCodeUKTZED/GetCodeUKTZED?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<IEnumerable<ProductCodeUKTZEDEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (IEnumerable<ProductCodeUKTZEDEntity>)result;
        }
    }
}
