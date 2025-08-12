using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProject.Helpers.Template.Paginator;
using ShopProjectSQLDataBase.Entities; 
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DataBaseController
{
    public class ObjectOwnerController
    {
        private HttpClient _httpClient;
        public ObjectOwnerController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<bool> DeleteObjectsOwner(string token, ObjectOwnerEntity item)
        {
            var content = JsonSerializer.Serialize(item);


            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ObjectOwner/DeleteObjectsOwner?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result;
        }

        public async Task<PaginatorData<ObjectOwnerEntity>> GetObjectsOwnersByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusObjectOwner status)
        {

            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ObjectOwner/GetObjectsOwnersByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<PaginatorData<ObjectOwnerEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (PaginatorData<ObjectOwnerEntity>)result;
        }

        public async Task<PaginatorData<ObjectOwnerEntity>> GetObjectsOwnersPageColumn(string token, int page, int countColumn, TypeStatusObjectOwner status)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ObjectOwner/GetObjectsOwnersPageColumn?token={token}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<PaginatorData<ObjectOwnerEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (PaginatorData<ObjectOwnerEntity>)result;
        }

        public async Task<IEnumerable<ObjectOwnerEntity>> GetObjectsOwners(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ObjectOwner/GetObjectsOwners?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<IEnumerable<ObjectOwnerEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (IEnumerable<ObjectOwnerEntity>)result; 
        }

        public async Task<bool> AddObjectOwner(string token, ObjectOwnerEntity item)
        { 
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ObjectOwner/AddObjectOwner?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result; 
        }

        public async Task<bool> AddObjectsOwners(string token, List<ObjectOwnerEntity> item)
        { 
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ObjectOwner/AddObjectsOwners?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result =  CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result; 
        }

    }
}
