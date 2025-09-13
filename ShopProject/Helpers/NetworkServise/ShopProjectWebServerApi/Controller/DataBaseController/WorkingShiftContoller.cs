using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.SalePage; 
using System; 
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController
{
    internal class WorkingShiftContoller
    {
        private HttpClient _httpClient;

        public WorkingShiftContoller(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<int> AddWorkingShift(string token, UIWorkingShiftModel shift)
        {
            var content = JsonSerializer.Serialize(shift.ToCreateWorkingShiftDto());

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/WorkingShift/AddWorkingShift?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<int>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (int)result;
        }

        public async Task<bool> UpdateWorkingShift(string token, UIWorkingShiftModel shift)
        {
            var content = JsonSerializer.Serialize(shift.ToUpdateWorkingShiftDto());

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/WorkingShift/UpdateWorkingShift?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result;
        }
    }
}
