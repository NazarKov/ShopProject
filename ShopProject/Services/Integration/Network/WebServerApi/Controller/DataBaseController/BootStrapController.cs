using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.BootStrap;
using System;
using System.Net.Http; 
using System.Threading.Tasks;
using ZXing.Aztec.Internal;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class BootStrapController
    {
        private HttpClient _httpClient;

        public BootStrapController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<StartDataDto>> GetStartData()
        {  
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/BootStrap/Get");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();

            var result = ApiResponse<StartDataDto>.Unpacking(responseBody);

            return result;
        }
        public async Task<ApiResponse<WorkingShiftStatusDto>> GetWorkingShiftData(Guid userId)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/BootStrap/GetWorkingShiftData?userID={userId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();

            var result = ApiResponse<WorkingShiftStatusDto>.Unpacking(responseBody);

            return result;
        }
    }
}
