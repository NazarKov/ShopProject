using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.GiftCertificate;
using ShopProject.Helpers.Template.Paginator;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class GiftCertificatesController
    {
        private HttpClient _httpClient;

        public GiftCertificatesController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<bool> UpdateParameterGiftCertificate(string token, string parameter, object value, UpdateGiftCertificateDto item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/GiftCertificates/UpdateParameterGiftCertificates?token={token}&parameter={parameter}&value={value.ToString()}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }
        public async Task<bool> UpdateGiftCertificate(string token, UpdateGiftCertificateDto item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/GiftCertificates/UpdateGiftCertificate?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }
        public async Task<bool> AddGiftCertificates(string token, CreateGiftCertificateDto item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/GiftCertificates/AddGiftCertificate?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }
        public async Task<GiftCertificateDto> GetGiftCertificateByBarCode(string token, string barCode, TypeStatusGiftCertificate status)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/GiftCertificates/GetGiftCertificateByBarCode?token={token}&barCode={barCode}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<GiftCertificateDto>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data;
        }
        public async Task<PaginatorData<GiftCertificateDto>> GetGiftCertificateByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusGiftCertificate status)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/GiftCertificates/GetGiftCertificatesByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<GiftCertificateDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<PaginatorData<GiftCertificateDto>> GetGiftCertificatePageColumn(string token, int page, int countColumn, TypeStatusGiftCertificate status)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/GiftCertificates/GetGiftCertificatesPageColumn?token={token}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<GiftCertificateDto>>.Unpacking(responseBody);

            return result.Data;
        }
    }
}
