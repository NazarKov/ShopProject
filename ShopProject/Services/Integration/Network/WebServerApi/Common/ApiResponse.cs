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
        [JsonPropertyName("Status")]
        public int Status { get; set; }
        [JsonPropertyName("Message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("Data")]
        public T? Data { get; set; }
        [JsonPropertyName("Error")]
        public string Error { get; set; } = string.Empty;
        [JsonPropertyName("Errors")]
        public List<string> Errors { get; set; } = new List<string>();
        [JsonPropertyName("ErrorType")]
        public int ErrorType { get; set; }
        [JsonPropertyName("Source")]
        public int Source { get; set; }

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
