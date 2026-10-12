using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.BootStrap;
using System;
using System.Net.Http; 
using System.Threading.Tasks; 

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class BootStrapController
    {
        private readonly HttpClient _httpClient;

        public BootStrapController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<StartDataDto>> GetStartData()
        {     
            return await SendHttpRequest.SendAsync<StartDataDto>(() => _httpClient.GetAsync($"/api/BootStrap/Get"));
        }
        public async Task<ApiResponse<WorkingShiftStatusDto>> GetWorkingShiftData(Guid userId)
        {
            return await SendHttpRequest.SendAsync<WorkingShiftStatusDto>(() => _httpClient.GetAsync($"/api/BootStrap/GetWorkingShiftData?userID={userId}")); 
        }
    }
}
