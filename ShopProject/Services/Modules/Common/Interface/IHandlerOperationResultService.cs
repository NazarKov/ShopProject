using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Common.Interface
{
    internal interface IHandlerOperationResultService
    {
        public Task<string> CheckOperaionResultErrorStatus<T>(OperationResult<T> result, Action action = null);
    }
}
