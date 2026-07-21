using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Model.Domain.Notification; 
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.OperationRecorder; 
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Domain.OperationRecorder.Interface; 
using ShopProject.Services.Modules.Mapping.OperationRecorder; 
using System; 
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.PointOfSale.OperationRecorder
{
    internal class CreateOperationRecorederViewModel : ViewModel<CreateOperationRecorederViewModel>, IСontrolView
    {
        private ICommand _createTaxObjectCommand;
        private ICommand _exitWindowCommand;
        private ICommand _clearWindowCommand;

        private IOperationRecorderService _operationRecorderService;

        public CreateOperationRecorederViewModel(IOperationRecorderService operationRecorderService)
        {
            _operationRecorderService = operationRecorderService;
            _error = string.Empty;
            _success = string.Empty;
            _operationRecorderModel = new OperationRecorderModel();

            _createTaxObjectCommand = CreateCommandAsync(CreateTaxObject);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });
            _clearWindowCommand = CreateCommand(ClearWindow);

            _successTextBlockVisibiliti = Visibility.Collapsed;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
        }

        public Action? CloseView { get; set; }

        private OperationRecorderModel _operationRecorderModel;
        public OperationRecorderModel OperationRecorderModel
        {
            get { return _operationRecorderModel; }
            set { _operationRecorderModel = value;  OnPropertyChanged(nameof(OperationRecorderModel)); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged(nameof(Error)); }
        }

        private string _success;
        public string Success
        {
            get { return _success; }
            set { _success = value; OnPropertyChanged(nameof(Success)); }
        }

        private Visibility _successTextBlockVisibiliti;
        public Visibility SuccessTextBlockVisibiliti
        {
            get { return _successTextBlockVisibiliti; }
            set { _successTextBlockVisibiliti = value; OnPropertyChanged(nameof(SuccessTextBlockVisibiliti)); }
        }

        private Visibility _errorTextBlockVisibiliti;
        public Visibility ErrorTextBlockVisibiliti
        {
            get { return _errorTextBlockVisibiliti; }
            set { _errorTextBlockVisibiliti = value; OnPropertyChanged(nameof(ErrorTextBlockVisibiliti)); }
        }
        public ICommand CreateTaxObjectCommand => _createTaxObjectCommand;

        public async Task CreateTaxObject()
        {
            var result = await _operationRecorderService.Add(OperationRecorderModel.ToOperationRecorder());
            if (result.IsSuccess)
            {
                SetSuccess(result.Data.Name);
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Каса", "Касовий апарат успішно створений в базі даних")));
                await MediatorService.ExecuteEventAsync(NavigationButton.ReloadOperationRecroder.ToString());

            }
            else if (result.IsError)
            {
                SetError(result.ErrorMessage);
            }
            else
            {
                SetError("Невдалося виконати операцію");
            }

        }
        public ICommand ClearWindowCommadn => _clearWindowCommand;
        private void ClearWindow()
        {
            OperationRecorderModel = new OperationRecorderModel();
        }
        public ICommand ExitWindowCommand => _exitWindowCommand;
        private void SetError(string error)
        {
            Error = error;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            ErrorTextBlockVisibiliti = Visibility.Visible;
        }
        private void SetSuccess(string name)
        {
            Success = $"Касовий апарат {name} створений";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
        }
    }
}
