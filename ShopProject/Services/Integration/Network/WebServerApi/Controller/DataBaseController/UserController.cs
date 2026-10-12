using ShopProject.Model.Enum; 
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.User;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator; 
using System; 
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json; 
using System.Threading.Tasks; 

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class UserController
    { 
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<PaginatorDto<UserDto,TypeStatusUser>>> GetByNamePageColumn(string name, PaginatorDto<UserDto, int> paginator)
        {
            var content = JsonSerializer.Serialize(paginator);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<UserDto, TypeStatusUser>>(() => _httpClient.PostAsync($"/api/User/GetByNamePageColumn?name={name}", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<UserDto, TypeStatusUser>>> GetPageColumn(PaginatorDto<UserDto,int> paginator)
        {

            var content = JsonSerializer.Serialize(paginator);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<UserDto, TypeStatusUser>>(() => _httpClient.PostAsync($"/api/User/GetPageColumn", httpContent)); 
        } 

        public async Task<ApiResponse<bool>> UpdateUser(UpdateUserDto user)
        {
            var content = JsonSerializer.Serialize(user);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/User/Update", httpContent)); 
        }

        public async Task<ApiResponse<UserDto>> Add(CreateUserDto user)
        { 
            var content = JsonSerializer.Serialize(user);
            
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await SendHttpRequest.SendAsync<UserDto>(() => _httpClient.PostAsync($"/api/User/Add", httpContent)); 
        } 
        public async Task<ApiResponse<AuthorizationUserDto>> Authorization(string login, string password)
        {
            return await SendHttpRequest.SendAsync<AuthorizationUserDto>(() => _httpClient.GetAsync($"/api/User/Authorization?login={login}&password={password}&devise={Environment.MachineName}")); 
        }

        public async Task<ApiResponse<UserDto>> GetUser(string token)
        {
            return await SendHttpRequest.SendAsync<UserDto>(() => _httpClient.GetAsync($"/api/User/GetByToken?token={token}")); 
        }
        public async Task<ApiResponse<bool>> UpdateParameter(string parameter, object value, UpdateUserDto user)
        {
            var content = JsonSerializer.Serialize(user);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/User/UpdateParameter?parameter={parameter}&value={value.ToString()}", httpContent)); 
        }
    }
}
