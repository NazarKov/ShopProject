using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.WorkingShift;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.Template.Paginator;
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

        public async Task<int> AddWorkingShift(string token, WorkingShift shift)
        {
            var content = JsonSerializer.Serialize(shift.ToCreateWorkingShiftDto());

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/WorkingShift/AddWorkingShift?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<int>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data;
        }

        public async Task<bool> UpdateWorkingShift(string token, WorkingShift shift)
        {
            var content = JsonSerializer.Serialize(shift.ToUpdateWorkingShiftDto());

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/WorkingShift/UpdateWorkingShift?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<bool>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data;
        }
        public async Task<WorkingShiftDto> GetWorkingShift(string token, string id)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/WorkingShift/GetWorkingShift?token={token}&id={id}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<WorkingShiftDto>.Unpacking(responseBody);
            return result.Data;
        }
    }
}
