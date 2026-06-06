using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Common
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("Status")]
        public int Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public T? Data { get; set; }
        [JsonPropertyName("Error")]
        public string? Error { get; set; }
        [JsonPropertyName("Errors")]
        public List<string>? Errors { get; set; }
        [JsonPropertyName("ErrorType")]
        public int ErrorType { get; set; }
        [JsonPropertyName("Source")]
        public int Source { get; set; }

        public static ApiResponse<T> Ok(T? data , string? message = null) 
        {
            return new ApiResponse<T> { Status = (int)ResponseStatus.Success, Data = data, Message = message };
        }

        public static ApiResponse<T> Fail(string error,ErrorType erroType = ShopProjectWebServer.Api.Common.ErrorType.None, ErrorSource errorSource = ErrorSource.None)
        {
            return new ApiResponse<T> { Status = (int)ResponseStatus.Error, Error = error , Source = (int)errorSource,ErrorType =(int)erroType };
        }

        public static ApiResponse<T> Fail(List<string> errors, ErrorType erroType = ShopProjectWebServer.Api.Common.ErrorType.None, ErrorSource errorSource = ErrorSource.None)
        {
            return new ApiResponse<T> { Status = (int)ResponseStatus.Error, Errors = errors, Source = (int)errorSource, ErrorType = (int)erroType };
        }
    }
}
