using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.WorkingShift;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.WorkingShift;
using ShopProject.Services.Modules.Mapping.WorkingShift; 
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class WorkingShiftContoller
    {
        private HttpClient _httpClient;

        public WorkingShiftContoller(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<WorkingShiftDto>> AddWorkingShift(WorkingShift shift)
        {
            var content = JsonSerializer.Serialize(shift.ToCreateWorkingShiftDto());

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/WorkingShift/Add", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<WorkingShiftDto>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result;
        }

        public async Task<ApiResponse<WorkingShiftDto>> UpdateWorkingShift(WorkingShift shift)
        {
            var content = JsonSerializer.Serialize(shift.ToUpdateWorkingShiftDto());

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/WorkingShift/Update", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<WorkingShiftDto>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();
            return result;
        }
        public async Task<ApiResponse<WorkingShiftDto>> GetWorkingShift(int id)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/WorkingShift/GetById?id={id}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<WorkingShiftDto>.Unpacking(responseBody);
            return result;
        }

        public async Task<ApiResponse<WorkingShiftResourseDto>> GetResourseById(string fiscalNumberRRo)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/WorkingShift/GetResourseByNumberRRO?fiscalNumberRRo={fiscalNumberRRo}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync(); 
            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<WorkingShiftResourseDto>.Unpacking(responseBody);
            return result;
        }
    }
}
