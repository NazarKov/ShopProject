using DocumentFormat.OpenXml.Wordprocessing;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ProductUnit;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class ProductUnitController
    {
        private HttpClient _httpClient;
        public ProductUnitController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<bool> AddProductUnit(string token, CreateProductUnitDto unit)
        {
            var content = JsonSerializer.Serialize(unit);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductUnit/AddUnit?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<bool> UpdateUnit(string token, UpdateProductUnitDto unit)
        {
            var content = JsonSerializer.Serialize(unit);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductUnit/UpdateUnit?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<bool> UpdateParameterUnit(string token, string parameter, object value, UpdateProductUnitDto product)
        {
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductUnit/UpdateParameterUnit?token={token}&parameter={parameter}&value={value.ToString()}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<bool> DeleteUnit(string token, ProductUnit unit)
        { 
            HttpContent httpContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductUnit/DeleteUnit?token={token}&id={unit.ID}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<Paginator<ProductUnitDto>> GetUnitByCode(string token, string code, int page, int countColumn, TypeStatusUnit statusUnit)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnitByCode?token={token}&code={code}&countColumn={countColumn}&page={page}&status={statusUnit}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<Paginator<ProductUnitDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<Paginator<ProductUnitDto>> GetUnitsByNamePageColumn(string token, string name,int page, int countColumn, TypeStatusUnit statusUnit)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnitsByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={statusUnit}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<Paginator<ProductUnitDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<Paginator<ProductUnitDto>> GetUnitsPageColumn(string token ,int page, int countColumn, TypeStatusUnit statusUnit)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnitsPageColumn?token={token}&countColumn={countColumn}&page={page}&status={statusUnit}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<Paginator<ProductUnitDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<IEnumerable<ProductUnitDto>> GetUnits(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnits?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<ProductUnitDto>>.Unpacking(responseBody);

            return result.Data; 
        }
    }
}
