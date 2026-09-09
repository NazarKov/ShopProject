using ShopProject.Services.Integration.Network.WebServerApi.Common;
using ShopProject.Services.Modules.Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.OperaionResult
{
    public static class OperationResultApiMappingExtensions
    {
        public static OperationResult<T> ToOperationResult<T>(this ApiResponse<T> item)
        {
            return new OperationResult<T>()
            {
                Source = (ShopProject.Services.Modules.Common.Enum.ErrorSource)item.Source,
                Status = (ShopProject.Services.Modules.Common.Enum.ResultStatus)item.Status,
                ErrorMessage = item.Error,
                ErrorType = (ShopProject.Services.Modules.Common.Enum.ErrorType)item.ErrorType,
                ValidationErrors = item.Errors,
                Data = item.Data,
            };
        }
    }
}
