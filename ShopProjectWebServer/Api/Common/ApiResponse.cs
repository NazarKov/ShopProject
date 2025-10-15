namespace ShopProjectWebServer.Api.Common
{
    public class ApiResponse<T>
    {
        public ResponseStatus  Status { get; set; }
        public string? Message { get; set; }

        public T? Data { get; set; }

        public List<string>? Errors { get; set; }

        public static ApiResponse<T> Ok(T? data , string? message = null) 
        {
            return new ApiResponse<T> { Status = ResponseStatus.Message, Data = data, Message = message };
        }

        public static ApiResponse<T> Fail(string error)
        {
            return new ApiResponse<T> { Status = ResponseStatus.Error, Errors = new List<string> { error } };
        }

        public static ApiResponse<T> Fail(List<string> errors)
        {
            return new ApiResponse<T> { Status = ResponseStatus.Error, Errors = errors };
        }
    }
}
