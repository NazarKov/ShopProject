using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.MediaAccessControl;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.SalePage;
using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> AddMAC(string token, UIMediaAccessControlModel item)
        { 
            var content = JsonSerializer.Serialize(item.ToCreatMediaAccessControlDto());
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/MediaAccessControl/AddMAC?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result; 
        }

        public async Task<MediaAccessControlDto> GetLastMAC(string token,Guid operationRecorderId)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/MediaAccessControl/GetLastMAC?token={token}&operationRecorderId={operationRecorderId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<MediaAccessControlDto>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (MediaAccessControlDto)result; 
        }
    }
}
