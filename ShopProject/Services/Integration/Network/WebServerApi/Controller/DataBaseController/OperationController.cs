using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Operation;
using ShopProject.Services.Integration.Network.WebServerApi.Common; 
using System.Net.Http;
using System.Text;
using System.Text.Json; 
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class OperationController
    { 
        private HttpClient _httpClient;
        public OperationController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<OperationDto>> Add(CreateOperationDto item)
        {
            var operation = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(operation, Encoding.UTF8, "application/json");

            return await SendHttpRequest.SendAsync<OperationDto>(() => _httpClient.PostAsync($"/api/Operation/Add", httpContent)); 
        }


        public async Task<ApiResponse<OperationІnformationDto>> GetLastNumberOperation(int shiftId)
        {
            return await SendHttpRequest.SendAsync<OperationІnformationDto>(() => _httpClient.GetAsync($"/api/Operation/GetLastNumberOperation?shiftId={shiftId}")); 
        }
        public async Task<ApiResponse<OperationInfoDto>> GetOperationsInfo(int shiftId)
        {
            return await SendHttpRequest.SendAsync<OperationInfoDto>(() => _httpClient.GetAsync($"/api/Operation/GetOperationsInfo?shiftId={shiftId}")); 
        }   
      
    }
}
