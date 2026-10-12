using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Common.Interface;
using System; 
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Services.Modules.Common
{
    internal class HandlerOperationResultSevice : IHandlerOperationResultService
    {
        public async Task<string> CheckOperaionResultErrorStatus<T>(OperationResult<T> result, Action action = null)
        {
            switch (result.ErrorType)
            {
                case Services.Modules.Common.Enum.ErrorType.Unauthorized:
                    {
                        await MediatorService.ExecuteEventAsync("ErrorUnAuthorization");
                        Application.Current.Dispatcher.Invoke(() => {
                            action?.Invoke();
                        });
                        return string.Empty;
                    }
                default:
                    {
                        return result.ErrorMessage;
                    }

            }
        }
    }
}
