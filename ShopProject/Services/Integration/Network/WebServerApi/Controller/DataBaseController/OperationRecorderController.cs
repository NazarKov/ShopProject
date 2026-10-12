using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorder;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.OperationRecorder;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator;  
using System.Collections.Generic; 
using System.Net.Http; 
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class OperationRecorderController
    { 
        private readonly HttpClient _httpClient;
        public OperationRecorderController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<IEnumerable<OperationRecorderDto>>> AddRange(IEnumerable<CreateOperationRecorderDto> item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            return await SendHttpRequest.SendAsync<IEnumerable<OperationRecorderDto>>(() => _httpClient.PostAsync($"/api/OperationRecorder/AddRange", httpContent)); 
        }
        public async Task<ApiResponse<OperationRecorderDto>> Add(CreateOperationRecorderDto item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<OperationRecorderDto>(() => _httpClient.PostAsync($"/api/OperationRecorder/Add", httpContent)); 
        }
        public async Task<ApiResponse<PaginatorDto<OperationRecorderDto, int>>> GetByNamePageColumn(string name,PaginatorDto<OperationRecorderDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<OperationRecorderDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            return await SendHttpRequest.SendAsync<PaginatorDto<OperationRecorderDto, int>>(() => _httpClient.PostAsync($"/api/OperationRecorder/GetByNamePageColumn?name={name}", httpContent)); 
        }

        public async Task<ApiResponse<OperationRecorderDto>> Update(UpdateOperationRecorderDto item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<OperationRecorderDto>(() => _httpClient.PostAsync($"/api/OperationRecorder/Update", httpContent)); 
        }

        public async Task<ApiResponse<bool>> UpdateParameter(string parameter, object value, string id)
        {
            var content = JsonSerializer.Serialize(id);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/OperationRecorder/UpdateParameter?parameter={parameter}&value={value.ToString()}", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<OperationRecorderDto, int>>> GetPageColumn(PaginatorDto<OperationRecorderDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<OperationRecorderDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<OperationRecorderDto, int>>(() => _httpClient.PostAsync($"/api/OperationRecorder/GetPageColumn", httpContent)); 
        } 
    }
}
