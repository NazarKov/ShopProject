using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.TaxObject;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject.Interface;
using ShopProject.Services.Modules.Mapping.TaxObject;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.PointOfSale.TaxObject
{
    internal class CreateTaxObjectViewModel :ViewModel<CreateTaxObjectViewModel>, IСontrolView
    {
        private ICommand _createTaxObjectCommand;
        private ICommand _exitWindowCommand;
        private ICommand _clearWindowCommand;

        private ITaxObjectService _taxObjectService;

        public CreateTaxObjectViewModel(ITaxObjectService taxObjectService)
        {
            _taxObjectService = taxObjectService;
            _error = string.Empty;
            _success = string.Empty;
            _taxObject = new TaxObjectModel();

            _createTaxObjectCommand = CreateCommandAsync(CreateTaxObject);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });
            _clearWindowCommand = CreateCommand(ClearWindow);

            _successTextBlockVisibiliti = Visibility.Collapsed;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
        }

        public Action? CloseView { get; set; }

        private TaxObjectModel _taxObject;
        public TaxObjectModel TaxObjectModel
        {
            get { return _taxObject; }
            set { _taxObject = value; OnPropertyChanged(nameof(TaxObjectModel)); }
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
            var result = await _taxObjectService.Add(TaxObjectModel.ToTaxObject());
            if (result.IsSuccess)
            {
                SetSuccess(result.Data.NameObject);
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Користувач", "Користувач успішно створений в базі даних")));
                await MediatorService.ExecuteEventAsync(NavigationButton.ReloadTaxObject.ToString());

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
            TaxObjectModel = new TaxObjectModel();
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
            Success = $"Обєкт {name} створений";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
        }
    }
}
