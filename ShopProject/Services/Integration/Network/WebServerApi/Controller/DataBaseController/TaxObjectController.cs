using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ObjectOwner;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorder;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.User;
using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.TaxObject;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.TaxObjectUser;
using System.Collections.Generic;
using System.Net.Http; 
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController
{
    public class TaxObjectController
    {
        private readonly HttpClient _httpClient;
        public TaxObjectController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<ApiResponse<PaginatorDto<TaxObjectDto, int>>> GetPageColumn(PaginatorDto<TaxObjectDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<TaxObjectDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<TaxObjectDto, int>>(() => _httpClient.PostAsync($"/api/TaxObject/GetPageColumn", httpContent)); 
        }

        public async Task<ApiResponse<PaginatorDto<TaxObjectDto, int>>> GetByNamePageColumn(string name, PaginatorDto<TaxObjectDto, int> paginator)
        {
            var content = JsonSerializer.Serialize<PaginatorDto<TaxObjectDto, int>>(paginator);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<PaginatorDto<TaxObjectDto, int>>(() => _httpClient.PostAsync($"/api/TaxObject/GetByNamePageColumn?name={name}", httpContent)); 
        }

        public async Task<ApiResponse<TaxObjectDto>> Add(CreateTaxObjectDto item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<TaxObjectDto>(() => _httpClient.PostAsync($"/api/TaxObject/Add", httpContent)); 
        }
        public async Task<ApiResponse<IEnumerable<TaxObjectDto>>> AddRange(IEnumerable<CreateTaxObjectDto> item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<IEnumerable<TaxObjectDto>>(() => _httpClient.PostAsync($"/api/TaxObject/AddRange", httpContent)); 
        }

        public async Task<ApiResponse<bool>> AddBindingOperationRecorder(string idTaxObject, IEnumerable<OperationRecorderDto> items)
        {
            var content = JsonSerializer.Serialize(items);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/TaxObject/AddBindingOperationRecorder?idTaxObject={idTaxObject}", httpContent)); 
        }

        public async Task<ApiResponse<bool>> AddBindingUser(string idTaxObject, IEnumerable<UserDto> items)
        {
            var content = JsonSerializer.Serialize(items);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/TaxObject/AddBindingUser?idTaxObject={idTaxObject}", httpContent)); 
        }

        public async Task<ApiResponse<IEnumerable<TaxObjectUserDto>>> GetTaxObjectsAssignedUser(string iduser)
        {  
            return await SendHttpRequest.SendAsync<IEnumerable<TaxObjectUserDto>>(() => _httpClient.GetAsync($"/api/TaxObject/GetTaxObjectsAssignedUser?iduser={iduser}")); 
        }

        public async Task<ApiResponse<bool>> Update(UpdateTaxObjectDto item)
        {
            var content = JsonSerializer.Serialize(item);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/TaxObject/Update", httpContent)); 
        }
        public async Task<ApiResponse<bool>> UpdateParameter(string parameter, object value, string id)
        {
            var content = JsonSerializer.Serialize(id);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await SendHttpRequest.SendAsync<bool>(() => _httpClient.PostAsync($"/api/TaxObject/UpdateParameter?parameter={parameter}&value={value.ToString()}", httpContent)); 
        } 
    }
}
