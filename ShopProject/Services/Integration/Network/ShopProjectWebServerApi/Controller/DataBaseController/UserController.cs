using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.User;
using ShopProject.Model.Enum;
using ShopProject.Model.Exceptions;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Common;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.User;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping;
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

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class UserController
    { 
        private HttpClient _httpClient;

        public UserController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<Paginator<UserDto>> GetUserByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUser status )
        {

            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUserByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={status }");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<Paginator<UserDto>>.Unpacking(responseBody);
            return result.Data;
        }

        public async Task<Paginator<UserDto>> GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status )
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUsersPageColumn?token={token}&countColumn={countColumn}&page={page}&status={status }");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<Paginator<UserDto>>.Unpacking(responseBody);
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

        public async Task<bool> UpdateUser(string token, User user)
        {
            var content = JsonSerializer.Serialize(user);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/UpdateUser?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<bool>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data;
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
            
            var result = new ApiResponse<AuthorizationUserDto>();
            
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = ApiResponse<AuthorizationUserDto>.Unpacking(responseBody);
                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                result = ApiResponse<AuthorizationUserDto>.Unpacking(responseBody); 
                if (result.Status == ResponseStatus.Error)
                {
                    throw new AuthorizationException(result?.Errors?.ElementAt(0) ?? "Не вдалося авторизуватися");
                }
            }  

            httpResponse.EnsureSuccessStatusCode(); 

            throw new AuthorizationException("Не вдалося авторизуватися");
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
