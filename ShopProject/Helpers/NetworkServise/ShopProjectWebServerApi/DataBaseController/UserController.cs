using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProject.Views.SettingPage;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DataBaseController
{
    public class UserController
    {
        private string _url;
        public UserController(string url)
        {
            _url = url;
        }

        public async Task<bool> AddUser(string token, UserEntity user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var content = JsonSerializer.Serialize(user);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"/api/User/AddUser?token={token}", httpContent);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<bool>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (bool)result;
            }
        }


        public async Task<TokenEntity> Authorization(string login, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/User/Authorization?login={login}&password={password}&devise={Environment.MachineName}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<TokenEntity>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (TokenEntity)result;
            }
        }

        public async Task<UserEntity> GetUser(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/User/GetUser?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<UserEntity>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (UserEntity)result;
            }
        }

        public async Task<UserEntity> GetUserById(string token , string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/User/GetUserById?token={token}&id={id}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<UserEntity>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (UserEntity)result;
            }
        }

        public async Task<IEnumerable<UserEntity>> GetUsers(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/User/GetUsers?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<IEnumerable<UserEntity>>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (IEnumerable<UserEntity>)result;
            }
        }

    }
}
