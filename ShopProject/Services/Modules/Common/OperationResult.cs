using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Common
{
    internal class OperationResult<TData>
    {
        public ResultStatus Status { get; set; }
        public TData? Data { get; set; } 
        public string ErrorMessage { get; set; } = string.Empty;
        public List<string> ValidationErrors { get; set; } = new List<string>(); 
        public ErrorType ErrorType { get; set; } 
        public ErrorSource? Source { get; set; }

        public bool IsSuccess => Status == ResultStatus.Success;
        public bool IsError => Status == ResultStatus.Error;
         

        public static OperationResult<TData> Success(TData data)
            => new OperationResult<TData> { Status = ResultStatus.Success, Data = data };

        public static OperationResult<TData> Fail(string message, ErrorType type = ErrorType.None)
            => new OperationResult<TData>
            {
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
