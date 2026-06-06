using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Services.Common.Enum;

namespace ShopProjectWebServer.Services.Common
{
    public class OperationResult<TData>
    {
        public ShopProjectWebServer.Services.Common.Enum.ResultStatus Status { get; set; }
        public TData? Data { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public List<string>? ValidationErrors { get; set; }
        public ShopProjectWebServer.Services.Common.Enum.ErrorType ErrorType { get; set; }
        public ShopProjectWebServer.Services.Common.Enum.ErrorSource Source { get; set; }

        public bool IsSuccess => Status == ResultStatus.Success;
        public bool IsError => Status == ResultStatus.Error;
         
        public static OperationResult<TData> Success(TData data)
            => new OperationResult<TData> { Status = ResultStatus.Success, Data = data };

        public static OperationResult<TData> Fail(string message, ShopProjectWebServer.Services.Common.Enum.ErrorType type = ShopProjectWebServer.Services.Common.Enum.ErrorType.None, ShopProjectWebServer.Services.Common.Enum.ErrorSource source = ShopProjectWebServer.Services.Common.Enum.ErrorSource.None)
            => new OperationResult<TData>
            {
                Source = source,
                Status = ResultStatus.Error,
                ErrorMessage = message,
                ErrorType = type
            };

        public static OperationResult<TData> ValidationFail(List<string> errors)
            => new OperationResult<TData>
            {
                Status = ResultStatus.Error,
                ValidationErrors = errors
            }; 
    }
}
