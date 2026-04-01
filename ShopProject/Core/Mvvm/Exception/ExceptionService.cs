using ShopProject.Core.Mvvm.ExceptionServise.Interface; 
using ShopProject.Core.Mvvm.Mediator.Notifications;
using ShopProject.Core.Mvvm.Service; 
using System; 
using System.Threading.Tasks;

namespace ShopProject.Core.Mvvm.Exception
{
    public class ExceptionService : IExceptionService
    {
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
        }
    }
}
