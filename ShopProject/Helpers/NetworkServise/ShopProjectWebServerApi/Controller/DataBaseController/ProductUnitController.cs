using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProject.Helpers.Template.Paginator;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
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

        public async Task<bool> AddProductUnit(string token, ProductUnitEntity unit)
        {
            var content = JsonSerializer.Serialize(unit);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductUnit/AddUnit?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = CheckingResponse.Unpacking<bool>(responseBody);

            return (bool)result;
        }

        public async Task<bool> UpdateUnit(string token, ProductUnitEntity unit)
        {
            var content = JsonSerializer.Serialize(unit);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductUnit/UpdateUnit?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = CheckingResponse.Unpacking<bool>(responseBody);

            return (bool)result;
        }

        public async Task<bool> UpdateParameterUnit(string token, string parameter, object value, ProductUnitEntity product)
        {
            var content = JsonSerializer.Serialize(product);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductUnit/UpdateParameterUnit?token={token}&parameter={parameter}&value={value.ToString()}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = CheckingResponse.Unpacking<bool>(responseBody);

            return (bool)result;
        }

        public async Task<bool> DeleteUnit(string token, ProductUnitEntity unit)
        {
            var content = JsonSerializer.Serialize(unit);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ProductUnit/DeleteUnit?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = CheckingResponse.Unpacking<bool>(responseBody);

            return (bool)result;
        }

        public async Task<ProductUnitEntity> GetUnitByCode(string token, string code,TypeStatusUnit statusUnit)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnitByCode?token={token}&code={code}&status={statusUnit}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<ProductUnitEntity>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (ProductUnitEntity)result;
        }

        public async Task<PaginatorData<ProductUnitEntity>> GetUnitsByNamePageColumn(string token, string name,int page, int countColumn, TypeStatusUnit statusUnit)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnitsByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={statusUnit}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<PaginatorData<ProductUnitEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (PaginatorData<ProductUnitEntity>)result;
        }

        public async Task<PaginatorData<ProductUnitEntity>> GetUnitsPageColumn(string token ,int page, int countColumn, TypeStatusUnit statusUnit)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnitsPageColumn?token={token}&countColumn={countColumn}&page={page}&status={statusUnit}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<PaginatorData<ProductUnitEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (PaginatorData<ProductUnitEntity>)result;
        }

        public async Task<IEnumerable<ProductUnitEntity>> GetUnits(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ProductUnit/GetUnits?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<IEnumerable<ProductUnitEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (IEnumerable<ProductUnitEntity>)result; 
        }
    }
}
