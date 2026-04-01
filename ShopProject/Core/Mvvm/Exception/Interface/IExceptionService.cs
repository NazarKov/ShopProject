using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Core.Mvvm.ExceptionServise.Interface
{
    internal interface IExceptionService
    {
        public void Handle(System.Exception ex, Action<string>? externalErrorHandel = null);
        public Task HandleAsync(System.Exception ex, Action<string>? externalErrorHandel = null);
    }
}
