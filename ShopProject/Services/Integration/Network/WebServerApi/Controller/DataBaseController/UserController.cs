using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.User;
using ShopProject.Model.Enum;
using ShopProject.Model.Exceptions;
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
                //result = ApiResponse<AuthorizationUserDto>.Unpacking(responseBody); 
                //if (result.Status == ResponseStatus.Error)
                //{
                //    throw new AuthorizationException(result?.Errors?.ElementAt(0) ?? "Не вдалося авторизуватися");
                //}
            }  

            httpResponse.EnsureSuccessStatusCode(); 

            throw new AuthorizationException("Не вдалося авторизуватися");
        }

        //public async Task<User> GetUser(string token)
        //{
        //    HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUser?token={token}");
        //    string responseBody = await httpResponse.Content.ReadAsStringAsync(); 
        //    httpResponse.EnsureSuccessStatusCode();

        //    var result = ApiResponse<UserDto>.Unpacking(responseBody);
        //    return result.Data.ToUser(); 
        //}

        //public async Task<User> GetUserById(string token , string id)
        //{
        //    HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUserById?token={token}&id={id}");
        //    string responseBody = await httpResponse.Content.ReadAsStringAsync();

        //    var result = ApiResponse<UserDto>.Unpacking(responseBody);
        //    httpResponse.EnsureSuccessStatusCode();

        //    return result.Data.ToUser();
        //}

        //public async Task<IEnumerable<User>> GetUsers(string token)
        //{
        //    HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/User/GetUsers?token={token}");
        //    string responseBody = await httpResponse.Content.ReadAsStringAsync();

        //    var result = ApiResponse<IEnumerable<UserDto>>.Unpacking(responseBody);
        //    httpResponse.EnsureSuccessStatusCode();

        //    return result.Data.ToUser(); 
        //}

    }
}
