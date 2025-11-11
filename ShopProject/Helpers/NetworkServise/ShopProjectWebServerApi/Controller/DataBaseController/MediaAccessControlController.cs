using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.MediaAccessControl; 
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.SalePage;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class MediaAccessControlController
    {
        private HttpClient _httpClient;
        public MediaAccessControlController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<bool> AddMAC(string token, MediaAccessControl item)
        { 
            var content = JsonSerializer.Serialize(item.ToCreatMediaAccessControlDto());
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/MediaAccessControl/AddMAC?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data; 
        }

        public async Task<MediaAccessControlDto> GetLastMAC(string token,Guid operationRecorderId)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/MediaAccessControl/GetLastMAC?token={token}&operationRecorderId={operationRecorderId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<MediaAccessControlDto>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data; 
        }
    }
}
