using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper;
using ShopProject.Helpers.Template.Paginator;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DataBaseController
{
    public class OperationRecorederController
    { 
        private HttpClient _httpClient;
        public OperationRecorederController(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url); 
        }

        public async Task<bool> DeleteOperationsRecorder(string token, OperationsRecorderEntity operationsRecorder)
        {
            var content = JsonSerializer.Serialize(operationsRecorder);


            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/DeleteOperationRecorder?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result;
        }

        public async Task<PaginatorData<OperationsRecorderEntity>> GetOperationsRecorderByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusOperationRecorder status )
        {

            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecordersByNamePageColumn?token={token}&name={name}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<PaginatorData<OperationsRecorderEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (PaginatorData<OperationsRecorderEntity>)result;
        }

        public async Task<PaginatorData<OperationsRecorderEntity>> GetOperationsRecorderPageColumn(string token, int page, int countColumn, TypeStatusOperationRecorder status )
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecordersPageColumn?token={token}&countColumn={countColumn}&page={page}&status={status}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<PaginatorData<OperationsRecorderEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (PaginatorData<OperationsRecorderEntity>)result;
        }

        public async Task<IEnumerable<OperationsRecorderEntity>> GetOperationRecorders(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecorders?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<IEnumerable<OperationsRecorderEntity>>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (IEnumerable<OperationsRecorderEntity>)result; 
        }

        public async Task<bool> AddOperationRecorder(string token, OperationsRecorderEntity item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/AddOperationRecorder?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result; 
        }

        public async Task<bool> AddOperationRecorders(string token, List<OperationsRecorderEntity> item)
        { 
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/AddOperationRecorders?token={token}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result; 
        }

        public async Task<bool> AddBindingOperationRecorder(string token, string idoperationrecoreder, string idobjectowner)
        {
            var content = JsonSerializer.Serialize(string.Empty);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/AddBindingOperationRecorder?token={token}&idoperationrecoreder={idoperationrecoreder}&idobjectowner={idobjectowner}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = CheckingResponse.Unpacking<bool>(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return (bool)result; 
        }

    }
}
