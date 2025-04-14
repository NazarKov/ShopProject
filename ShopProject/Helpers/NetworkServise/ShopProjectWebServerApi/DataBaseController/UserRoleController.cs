using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DataBaseController
{
    public class UserRoleController
    {
        private string _url;
        public UserRoleController(string url)
        {
            _url = url;
        }
        public async Task<IEnumerable<UserRoleEntity>> GetRoles(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/UserRole/GetRoles?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<IEnumerable<UserRoleEntity>>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (IEnumerable<UserRoleEntity>)result;
            }
        }
    }
}
