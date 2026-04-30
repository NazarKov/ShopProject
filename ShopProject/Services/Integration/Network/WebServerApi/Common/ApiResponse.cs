using Microsoft.Identity.Client.NativeInterop;
using ShopProject.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Common
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("status")]
        public ResponseStatus Status { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("data")]
        public T? Data { get; set; }
        [JsonPropertyName("errors")]
        public List<string>? Errors { get; set; }

        public static ApiResponse<T> Unpacking(string json)
        { 
            var result = JsonSerializer.Deserialize<ApiResponse<T>>(json);
            if (result == null) 
            {
                throw new System.Exception("Невдалося розпакувати пакет");
            }  
            return result;
        } 
    }
}
