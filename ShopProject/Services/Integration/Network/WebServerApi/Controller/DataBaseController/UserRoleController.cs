using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.UserRole;
using ShopProject.Services.Integration.Network.WebServerApi.Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class UserRoleController
    {
        private HttpClient _httpClient;
        public UserRoleController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<UserRoleDto>> GetRoles(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/UserRole/GetRoles?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<IEnumerable<UserRoleDto>>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data;
        }
    }
}
