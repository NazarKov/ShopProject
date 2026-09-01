using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.User;
using ShopProject.Model.Enum;
using ShopProject.Model.Exceptions;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Product;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.User;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator;
using ShopProject.Services.Modules.Mapping.User;
using System;
using System.Collections.Generic; 
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json; 
using System.Threading.Tasks; 

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class UserController
    { 
        private HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<PaginatorDto<UserDto,TypeStatusUser>>> GetByNamePageColumn(string name, PaginatorDto<UserDto, int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/GetByNamePageColumn?name={name}",httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorDto<UserDto, TypeStatusUser>>.Unpacking(responseBody);
            return result;
        }

        public async Task<ApiResponse<PaginatorDto<UserDto, TypeStatusUser>>> GetPageColumn(PaginatorDto<UserDto,int> paginator)
        {

            var content = JsonSerializer.Serialize(paginator);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/GetPageColumn", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<PaginatorDto<UserDto, TypeStatusUser>>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();  
            return result;
        } 

        public async Task<ApiResponse<bool>> UpdateUser(UpdateUserDto user)
        {
            var content = JsonSerializer.Serialize(user);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/Update", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<bool>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result;
        }

        public async Task<ApiResponse<UserDto>> Add(CreateUserDto user)
        { 
            var content = JsonSerializer.Serialize(user);
            
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json"); 
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/Add",httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            
            httpResponse.EnsureSuccessStatusCode(); 
            var result = ApiResponse<UserDto>.Unpacking(responseBody);
            return result; 
        } 
        public async Task<ApiResponse<AuthorizationUserDto>> Authorization(string login, string password)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/Authorization?login={login}&password={password}&devise={Environment.MachineName}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync(); 
            httpResponse.EnsureSuccessStatusCode(); 
            var result = ApiResponse<AuthorizationUserDto>.Unpacking(responseBody); 
            return result;
        }

        public async Task<ApiResponse<UserDto>> GetUser(string token)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetByToken?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode(); 
            var result = ApiResponse<UserDto>.Unpacking(responseBody);
            return result;
        }
        public async Task<ApiResponse<bool>> UpdateParameter(string parameter, object value, UpdateUserDto user)
        {
            var content = JsonSerializer.Serialize(user);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/User/UpdateParameter?parameter={parameter}&value={value.ToString()}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result;
        }
    }
}
