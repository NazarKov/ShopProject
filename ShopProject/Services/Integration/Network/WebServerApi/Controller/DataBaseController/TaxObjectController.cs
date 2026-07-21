using DocumentFormat.OpenXml.Wordprocessing;
using ShopProject.Model.Domain.TaxObject;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ObjectOwner;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorder;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.User;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.TaxObject;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.TaxObjectUser;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class TaxObjectController
    {
        private HttpClient _httpClient;
        public TaxObjectController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<PaginatorDto<TaxObjectDto, int>>> GetPageColumn(PaginatorDto<TaxObjectDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<TaxObjectDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/TaxObject/GetPageColumn",httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();

            var result = ApiResponse<PaginatorDto<TaxObjectDto, int>>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<PaginatorDto<TaxObjectDto, int>>> GetByNamePageColumn(string name, PaginatorDto<TaxObjectDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<TaxObjectDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/TaxObject/GetByNamePageColumn?name={name}",httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<PaginatorDto<TaxObjectDto, int>>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<TaxObjectDto>> Add(CreateTaxObjectDto item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/TaxObject/Add", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<TaxObjectDto>.Unpacking(responseBody);

            return result;
        }
        public async Task<ApiResponse<IEnumerable<TaxObjectDto>>> AddRange(IEnumerable<CreateTaxObjectDto> item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/TaxObject/AddRange", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<TaxObjectDto>>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<bool>> AddBindingOperationRecorder(string idTaxObject, IEnumerable<OperationRecorderDto> items)
        {
            var content = JsonSerializer.Serialize(items);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/TaxObject/AddBindingOperationRecorder?idTaxObject={idTaxObject}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<bool>> AddBindingUser(string idTaxObject, IEnumerable<UserDto> items)
        {
            var content = JsonSerializer.Serialize(items);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/TaxObject/AddBindingUser?idTaxObject={idTaxObject}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result;
        }

        public async Task<ApiResponse<IEnumerable<TaxObjectUserDto>>> GetTaxObjectsAssignedUser(string iduser)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/TaxObject/GetTaxObjectsAssignedUser?iduser={iduser}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<IEnumerable<TaxObjectUserDto>>.Unpacking(responseBody);

            return result;
        }



        public async Task<bool> DeleteObjectsOwner(string token, TaxObject item)
        {
            var content = JsonSerializer.Serialize(item);


            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponse = await _httpClient.PostAsync($"/api/ObjectOwner/DeleteObjectsOwner?token={token}&id={item.ID}", httpContent);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
            var result = ApiResponse<bool>.Unpacking(responseBody);

            return result.Data;
        }

      

        public async Task<IEnumerable<TaxObjectDto>> GetObjectsOwners(string token)
        { 
            HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/ObjectOwner/GetObjectsOwners?token={token}");
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            var result = ApiResponse<IEnumerable<TaxObjectDto>>.Unpacking(responseBody);
            httpResponse.EnsureSuccessStatusCode();

            return result.Data; 
        }

      

        

    }
}
