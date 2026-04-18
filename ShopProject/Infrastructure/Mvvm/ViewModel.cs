using ShopProject.Core.Mvvm.Command; 
using ShopProject.Services.Infrastructure.Exception;
using ShopProject.Services.Infrastructure.Exception.Interface;
using ShopProject.Services.Infrastructure.Logging;
using ShopProject.Services.Integration.Directory;
using ShopProject.Services.Integration.File.BaseFile;
using System; 
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

namespace ShopProject.Core.Mvvm
{
    public abstract class ViewModel<TViewModel> : INotifyPropertyChanged
    {
        private IExceptionService _exceptionService = new ExceptionService(new FileLoggerService(new DirectoryService(),new FileService()));
        static ViewModel()
        {

        } 

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected ICommand CreateCommand(Action func, Action<string>? externalErrorHandel = null)
        {
            return new DelegateCommand(() =>
            {
                try
                {
                    func();
                }
                catch (System.Exception ex)
                {
                    Task.Run(async () =>
                    {

                        await _exceptionService.HandleAsync(ex, externalErrorHandel);
                    });
                }
            });
        }
        protected ICommand CreateCommandParameter<T>(Action<T> func, Action<string>? externalErrorHandel = null)
        {
            return new DelegateParameterCommand<T>((parameter) =>
            {
                try
                {
                    func.Invoke(parameter);
                }
                catch (System.Exception ex)
                {
                    Task.Run(async () =>
                    {

                        await _exceptionService.HandleAsync(ex, externalErrorHandel);
                    });
                }
            });
        }

        protected ICommand CreateCommandParameterAsync<T>(Func<T,Task> func, Action<string>? externalErrorHandel = null)
        {
            return new DelegateParameterCommandAsync<T>(async (T) =>
            {
                try
                {
                    await func.Invoke(T);
                }
                catch (System.Exception ex)
                {
                    await _exceptionService.HandleAsync(ex, externalErrorHandel);
                }
            });
        }


        protected ICommand CreateCommandAsync(Func<Task> func, Action<string>? externalErrorHandel = null)
        {
            return new DelegateCommandAsync(async () =>
            {
                try
                {
                    await func();
                }
                catch (System.Exception ex)
                {
                    await _exceptionService.HandleAsync(ex, externalErrorHandel);
                }
            });

        }

        protected void SafeExecute(Action func, Action<string>? externalErrorHandel = null)
        {
            try
            {
                func(); 
            }
            catch (System.Exception ex)
            {
                _exceptionService.Handle(ex, externalErrorHandel);  
            }
        }
        protected async Task SafeExecuteAsync(Func<Task> func, Action<string>? externalErrorHandel = null)
        {
            try
            {
                 await func();
            }
            catch (System.Exception ex)
            {
                await _exceptionService.HandleAsync(ex, externalErrorHandel);
            }
        }


    }
}
