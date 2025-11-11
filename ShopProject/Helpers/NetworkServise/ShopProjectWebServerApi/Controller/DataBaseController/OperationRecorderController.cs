using Microsoft.VisualBasic.ApplicationServices;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.OperationRecorder; 
using ShopProject.Helpers.Template.Paginator;
using ShopProject.UIModel.OperationRecorderPage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController
{
    public class OperationRecorderController
    { 
        private HttpClient _httpClient;
        public OperationRecorderController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }
        public async Task<IEnumerable<OperationRecorderDto>> GetOperationRecordersByNumberAndUser(string token, string number,  Guid userId)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecordersByNumberAndUser?token={token}&number={number}&userId={userId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<OperationRecorderDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<IEnumerable<OperationsRecorderEntity>> GetOperationRecordersByNameAndUser(string token, string name, Guid userId)
        {

            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecordersByNameAndUser?token={token}&name={name}&userId={userId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<OperationsRecorderEntity>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<bool> DeleteOperationsRecorder(string token, OperationRecorder operationsRecorder)
        {
            var content = string.Empty;  
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/DeleteOperationRecorder?token={token}&id={operationsRecorder.ID}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<PaginatorData<OperationRecorderDto>> GetOperationsRecorderByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusOperationRecorder status )
        {

            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecordersByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<OperationRecorderDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<PaginatorData<OperationRecorderDto>> GetOperationsRecorderPageColumn(string token, int page, int countColumn, TypeStatusOperationRecorder status )
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecordersPageColumn?token={token}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorData<OperationRecorderDto>>.Unpacking(responseBody);

            return result.Data;
        }

        public async Task<IEnumerable<OperationRecorderDto>> GetOperationRecorders(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecorders?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<OperationRecorderDto>>.Unpacking(responseBody);

            return result.Data; 
        }

        public async Task<bool> AddOperationRecorder(string token, OperationsRecorderEntity item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/AddOperationRecorder?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data; 
        }

        public async Task<bool> AddOperationRecorders(string token, List<CreateOperationRecorderDto> item)
        { 
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/AddOperationRecorders?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<bool>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data; 
        }

        public async Task<bool> AddBindingOperationRecorder(string token, string idoperationrecoreder, string idobjectowner)
        {
            var content = JsonSerializer.Serialize(string.Empty);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/AddBindingOperationRecorder?token={token}&idoperationrecoreder={idoperationrecoreder}&idobjectowner={idobjectowner}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<bool>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data; 
        }

    }
}
