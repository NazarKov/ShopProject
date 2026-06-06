using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.SignatureKey;
using ShopProject.Services.Integration.Network.WebServerApi.Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class SignatureKeyController
    {
        private HttpClient _httpClient;

        public SignatureKeyController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
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
