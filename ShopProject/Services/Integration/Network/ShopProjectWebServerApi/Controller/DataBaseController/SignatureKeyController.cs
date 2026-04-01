using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Common;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.SignatureKey;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class SignatureKeyController
    {
        private HttpClient _httpClient;

        public SignatureKeyController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<SignatureKeyDto> GetKey(string token)
        {

            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ElectronicSignatureKey/GetElectronicSignatureKey?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<SignatureKeyDto>.Unpacking(responseBody);
            return result.Data;
        }
    }
}
