using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.Views.SettingPage;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DataBaseController
{
    public class UserController
    { 
        private HttpClient _httpClient;

        public UserController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<PaginatorData<UserEntity>> GetUserByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUser status )
        {

            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUserByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={status }");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<PaginatorData<UserEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (PaginatorData<UserEntity>)result;
        }

        public async Task<PaginatorData<UserEntity>> GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status )
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUsersPageColumn?token={token}&countColumn={countColumn}&page={page}&status={status }");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<PaginatorData<UserEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (PaginatorData<UserEntity>)result;
        }

        public async Task<bool> DeleteUser(string token, UserEntity user)
        { 
            var content = JsonSerializer.Serialize(user);


            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/DeleteUser?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result;
        }

        public async Task<bool> UpdateUser(string token, UserEntity user)
        {
            var content = JsonSerializer.Serialize(user);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/UpdateUser?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result;
        }

        public async Task<bool> AddUser(string token, UserEntity user)
        { 
            var content = JsonSerializer.Serialize(user);
            
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/AddUser?token={token}",httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            
            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();
           
            return (bool)result; 
        }


        public async Task<TokenEntity> Authorization(string login, string password)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/Authorization?login={login}&password={password}&devise={Environment.MachineName}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<TokenEntity>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (TokenEntity)result; 
        }

        public async Task<UserEntity> GetUser(string token)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUser?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<UserEntity>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (UserEntity)result; 
        }

        public async Task<UserEntity> GetUserById(string token , string id)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUserById?token={token}&id={id}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<UserEntity>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (UserEntity)result;
        }

        public async Task<IEnumerable<UserEntity>> GetUsers(string token)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUsers?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<IEnumerable<UserEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (IEnumerable<UserEntity>)result; 
        }

    }
}
