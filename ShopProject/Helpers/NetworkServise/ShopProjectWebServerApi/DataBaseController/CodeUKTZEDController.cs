using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DataBaseController
{
    public class CodeUKTZEDController
    {
        private string _url;
        public CodeUKTZEDController(string url)
        {
            _url = url;
        }

        public async Task<IEnumerable<CodeUKTZEDEntity>> GetCodeUKTZED(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/CodeUKTZED/GetCodeUKTZED?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<IEnumerable<CodeUKTZEDEntity>>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (IEnumerable<CodeUKTZEDEntity>)result;
            }
        }
    }
}
