using Microsoft.VisualBasic.ApplicationServices;
using ShopProject.Model.Domain.OperationRecorder;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorder;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator;
using ShopProject.View.TemplatePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class OperationRecorderController
    { 
        private HttpClient _httpClient;
        public OperationRecorderController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<IEnumerable<OperationRecorderDto>>> AddRange(IEnumerable<CreateOperationRecorderDto> item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/AddRange", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<IEnumerable<OperationRecorderDto>>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result;
        }
        public async Task<ApiResponse<OperationRecorderDto>> Add(CreateOperationRecorderDto item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/Add", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<OperationRecorderDto>.Unpacking(responseBody);

            return result;
        }
        public async Task<ApiResponse<PaginatorDto<OperationRecorderDto, int>>> GetByNamePageColumn(string name,PaginatorDto<OperationRecorderDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<OperationRecorderDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/GetByNamePageColumn?name={name}",httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorDto<OperationRecorderDto, int>>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<PaginatorDto<OperationRecorderDto, int>>> GetPageColumn(PaginatorDto<OperationRecorderDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<OperationRecorderDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/OperationRecorder/GetPageColumn",httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorDto<OperationRecorderDto, int>>.Unpacking(responseBody);

            return result;
        }


        /// <summary>
        /// 
        /// </summary> 

        public async Task<IEnumerable<OperationRecorderDto>> GetOperationRecordersByNumberAndUser(string token, string number,  Guid userId)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecordersByNumberAndUser?token={token}&number={number}&userId={userId}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<OperationRecorderDto>>.Unpacking(responseBody);

            return result.Data;
        }

        //public async Task<IEnumerable<OperationsRecorderEntity>> GetOperationRecordersByNameAndUser(string token, string name, Guid userId)
        //{

        //    HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecordersByNameAndUser?token={token}&name={name}&userId={userId}");
        //    string responseBody = await httpResponse.Content.ReadAsStringAsync();

        //    httpResponse.EnsureSuccessStatusCode();
        //    var result = ApiResponse<IEnumerable<OperationsRecorderEntity>>.Unpacking(responseBody);

        //    return result.Data;
        //}

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

      

        public async Task<IEnumerable<OperationRecorderDto>> GetOperationRecorders(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/OperationRecorder/GetOperationRecorders?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<OperationRecorderDto>>.Unpacking(responseBody);

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
