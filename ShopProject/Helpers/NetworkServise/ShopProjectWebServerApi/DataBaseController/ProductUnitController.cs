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
    public class ProductUnitController
    {
        private string _url;
        public ProductUnitController(string url)
        {
            _url = url;
        }

        public async Task<IEnumerable<ProductUnitEntity>> GetUnits(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                HttpResponseMessage httpResponse = await client.GetAsync($"/api/ProductUnit/GetUnits?token={token}");
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var result = CheckingResponse.Unpacking<IEnumerable<ProductUnitEntity>>(responseBody);
                httpResponse.EnsureSuccessStatusCode();

                return (IEnumerable<ProductUnitEntity>)result;
            }
        }
    }
}
