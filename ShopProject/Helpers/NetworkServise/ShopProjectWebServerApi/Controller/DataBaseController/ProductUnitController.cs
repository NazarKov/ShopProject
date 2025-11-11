using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.ProductUnit; 
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
            var content = JsonSerializer.Serialize(unit);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductUnit/DeleteUnit?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<ProductUnitDto> GetUnitByCode(string token, string code,TypeStatusUnit statusUnit)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnitByCode?token={token}&code={code}&status={statusUnit}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<ProductUnitDto>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<PaginatorData<ProductUnitDto>> GetUnitsByNamePageColumn(string token, string name,int page, int countColumn, TypeStatusUnit statusUnit)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnitsByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={statusUnit}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<ProductUnitDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<PaginatorData<ProductUnitDto>> GetUnitsPageColumn(string token ,int page, int countColumn, TypeStatusUnit statusUnit)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnitsPageColumn?token={token}&countColumn={countColumn}&page={page}&status={statusUnit}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<ProductUnitDto>>.Unpacking(responseBody);

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
