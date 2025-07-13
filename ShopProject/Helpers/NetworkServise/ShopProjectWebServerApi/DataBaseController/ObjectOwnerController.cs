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
    public class ObjectOwnerController
    {
        private string _url;
        public ObjectOwnerController(string url)
        {
            _url = url;
        }

        public async Task<IEnumerable<ObjectOwnerEntity>> GetObjectsOwners(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/ObjectOwner/GetObjectsOwners?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<IEnumerable<ObjectOwnerEntity>>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (IEnumerable<ObjectOwnerEntity>)result;
            }
        }

        public async Task<bool> AddObjectOwner(string token, ObjectOwnerEntity item)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var content = JsonSerializer.Serialize(item);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"/api/ObjectOwner/AddObjectOwner?token={token}", httpContent);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<bool>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (bool)result;
            }
        }

        public async Task<bool> AddObjectsOwners(string token, List<ObjectOwnerEntity> item)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var content = JsonSerializer.Serialize(item);
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"/api/ObjectOwner/AddObjectsOwners?token={token}", httpContent);
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result =  CheckingResponse.Unpacking<bool>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (bool)result;
            }
        }

    }
}
