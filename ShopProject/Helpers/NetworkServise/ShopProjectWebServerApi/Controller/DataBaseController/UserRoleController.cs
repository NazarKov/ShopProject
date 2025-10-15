using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.UserRole;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProjectDataBase.Entities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class UserRoleController
    {
        private string _url;
        public UserRoleController(string url)
        {
            _url = url;
        }
        public async Task<IEnumerable<UserRoleDto>> GetRoles(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/UserRole/GetRoles?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = ApiResponse<IEnumerable<UserRoleDto>>.Unpacking(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return result.Data;
            }
        }
    }
}
