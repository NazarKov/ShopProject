using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.OperaionResult
{
    public static class OperationResultFiscalApiMappingExtensions
    {
        public static OperationResult<string> ToOperationResult(this FiscalServerApi.Services.Common.OperationResult<string> item)
        {
            var result = new OperationResult<string>()
            {
                Data = item.Data, 
                Status = (ResultStatus)item.Status,
                ErrorMessage = item.ErrorMessage,
                ErrorType = (ErrorType)item.ErrorType,
                ValidationErrors = item.ValidationErrors,
            };
            if (item.Source != null)
            {
                result.Source = (ErrorSource)item.Source;
            }
            return result;
        }
    }
}
