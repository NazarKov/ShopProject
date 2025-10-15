namespace ShopProjectWebServer.Api.Common
{
    public class ApiResponseDto<T>
    {
        public ResponseStatus  Status { get; set; }
        public string? Message { get; set; }

        public T? Data { get; set; }

        public List<string>? Errors { get; set; }

        public static ApiResponseDto<T> Ok(T? data , string? message = null) 
        {
            return new ApiResponseDto<T> { Status = ResponseStatus.Message, Data = data, Message = message };
        }

        public static ApiResponseDto<T> Fail(string error)
        {
            return new ApiResponseDto<T> { Status = ResponseStatus.Error, Errors = new List<string> { error } };
        }

        public static ApiResponseDto<T> Fail(List<string> errors)
        {
            return new ApiResponseDto<T> { Status = ResponseStatus.Error, Errors = errors };
        }
    }
}
