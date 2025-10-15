using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Common
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
            return JsonSerializer.Deserialize<ApiResponse<T>>(json);
        } 
    }
}
