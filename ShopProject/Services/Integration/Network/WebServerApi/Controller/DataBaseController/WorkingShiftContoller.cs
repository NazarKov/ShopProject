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
        private readonly HttpClient _httpClient;

        public WorkingShiftContoller(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<WorkingShiftDto>> AddWorkingShift(WorkingShift shift)
        {
            var content = JsonSerializer.Serialize(shift.ToCreateWorkingShiftDto());

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await SendHttpRequest.SendAsync<WorkingShiftDto>(() => _httpClient.PostAsync($"/api/WorkingShift/Add", httpContent)); 
        }

        public async Task<ApiResponse<WorkingShiftDto>> UpdateWorkingShift(WorkingShift shift)
        {
            var content = JsonSerializer.Serialize(shift.ToUpdateWorkingShiftDto());

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await SendHttpRequest.SendAsync<WorkingShiftDto>(() => _httpClient.PostAsync($"/api/WorkingShift/Update", httpContent)); 
        }
        public async Task<ApiResponse<WorkingShiftDto>> GetWorkingShift(int id)
        {
            return await SendHttpRequest.SendAsync<WorkingShiftDto>(() => _httpClient.GetAsync($"/api/WorkingShift/GetById?id={id}")); 
        }

        public async Task<ApiResponse<WorkingShiftResourseDto>> GetResourseById(string fiscalNumberRRo)
        {
            return await SendHttpRequest.SendAsync<WorkingShiftResourseDto>(() => _httpClient.GetAsync($"/api/WorkingShift/GetResourseByNumberRRO?fiscalNumberRRo={fiscalNumberRRo}")); 
        }
    }
}
