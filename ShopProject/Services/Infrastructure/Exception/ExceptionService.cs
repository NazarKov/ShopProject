using ShopProject.Services.Infrastructure.Exception.Interface;
using ShopProject.Services.Infrastructure.Logging.Interface;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using System;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.Exception
{
    internal class ExceptionService : IExceptionService
    {
        private ILoggerService _loggerService;

        public ExceptionService(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public void Handle(System.Exception ex, Action<string>? externalErrorHandel = null)
        {
            if (ex is IException exeption)
            {
                if (externalErrorHandel != null)
                {
                    externalErrorHandel.Invoke(exeption.Result.Content);
                }
                MediatorService.PublishNotifications<ShowNotificationEvent>(new ShowNotificationEvent(exeption.Result));
            }
            else
            {
                if (externalErrorHandel != null)
                {
                    externalErrorHandel.Invoke("Невдалося виконати операцію");
                }
                MediatorService.PublishNotifications<ShowNotificationEvent>(new ShowNotificationEvent(new BaseNotification() { Title = "Error", Content = ex.Message }));
            }
            _loggerService.WriteLog("[Data:" + DateTime.Now + "] " + "[Where]" + ex.StackTrace + "\n[Error] " + ex.Message);
        }

        public async Task HandleAsync(System.Exception ex , Action<string>? externalErrorHandel = null)
        {
            if (ex is IException exeption)
            {
                if (externalErrorHandel != null) 
                {
                    externalErrorHandel.Invoke(exeption.Result.Content);
                }
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(exeption.Result));
            }
            else
            {
                if (externalErrorHandel != null)
                {
                    externalErrorHandel.Invoke("Невдалося виконати операцію");
                }
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(new BaseNotification() { Title = "Error", Content = ex.Message }));
            }
            _loggerService.WriteLog("[Data:" + DateTime.Now + "] " + "[Where]" + ex.StackTrace + "\n[Error] " + ex.Message);
        }
    }
}
