using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller
{
    public class SettingsController
    {
        private string _url;

        public SettingsController(string url)
        {
            _url = url;
        }
        public async Task<string> Ping()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                HttpResponseMessage responseMessage = await client.GetAsync("/api/Settings/Ping");

                string responseBody = await responseMessage.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<string>(responseBody);
                responseMessage.EnsureSuccessStatusCode();

                return result.ToString();
            }
        }
    }
}
