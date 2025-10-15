using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Token;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.User;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.UIModel.UserPage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
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

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class UserController
    { 
        private HttpClient _httpClient;

        public UserController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<PaginatorData<UserDto>> GetUserByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUser status )
        {

            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUserByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={status }");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<UserDto>>.Unpacking(responseBody);
            return result.Data;
        }

        public async Task<PaginatorData<UserDto>> GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status )
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUsersPageColumn?token={token}&countColumn={countColumn}&page={page}&status={status }");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<PaginatorData<UserDto>>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode(); 

            return result.Data;
        }

        public async Task<bool> DeleteUser(string token, string userId)
        {   
            HttpContent httpContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/DeleteUser?token={token}&id={userId}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<bool>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data;
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

        public async Task<bool> AddUser(string token, CreateUserDto user)
        { 
            var content = JsonSerializer.Serialize(user);
            
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/AddUser?token={token}",httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            
            var result = ApiResponse<bool>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();
           
            return result.Data; 
        } 
        public async Task<AuthorizationUserDto> Authorization(string login, string password)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/Authorization?login={login}&password={password}&devise={Environment.MachineName}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
 
            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<AuthorizationUserDto>.Unpacking(responseBody);
            return result.Data;
        }

        public async Task<User> GetUser(string token)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUser?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync(); 
            httpResponse.EnsureSuccessStatusCode();

            var result = ApiResponse<UserDto>.Unpacking(responseBody);
            return result.Data.ToUser(); 
        }

        public async Task<User> GetUserById(string token , string id)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUserById?token={token}&id={id}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<UserDto>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data.ToUser();
        }

        public async Task<IEnumerable<User>> GetUsers(string token)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUsers?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<IEnumerable<UserDto>>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data.ToUser(); 
        }

    }
}
