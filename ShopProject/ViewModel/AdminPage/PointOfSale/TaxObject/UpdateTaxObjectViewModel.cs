using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.TaxObject;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject.Interface;
using ShopProject.Services.Modules.Mapping.TaxObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.PointOfSale.TaxObject
{
    internal class UpdateTaxObjectViewModel : ViewModel<UpdateTaxObjectViewModel>, IСontrolView, IViewModelLoadResourse
    {
        public ICommand _updateTaxObjectCommand;
        private ICommand _exitWindowCommand;
        private ITaxObjectService _taxObjectService;

        public UpdateTaxObjectViewModel(ITaxObjectService taxObjectService)
        {
            _taxObjectService = taxObjectService;

            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });
            _updateTaxObjectCommand = CreateCommandAsync(async ()=> { await UpdateTaxObject(); });

            _taxObject = new TaxObjectModel();
            _error = string.Empty;
            _success = string.Empty;
            _successTextBlockVisibiliti = Visibility.Collapsed;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
        } 
        public Action? CloseView { get; set; } 
        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);
            return Task.CompletedTask;
        }

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

        private void SetFieldPage()
        {
            var item = _taxObjectService.GetBindingTaxObjectOnSession();
            if (item != null)
            {
                TaxObjectModel = item.ToTaxObjectModel();
            }
        }

        public ICommand UpdateTaxObjectCommand => _updateTaxObjectCommand;
        private async Task UpdateTaxObject()
        {
            var result = await _taxObjectService.Update(TaxObjectModel.ToTaxObject());
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

        public ICommand ExitWindowCommand => _exitWindowCommand;
        private void SetError(string error)
        {
            Error = error;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            ErrorTextBlockVisibiliti = Visibility.Visible;
        }
        private void SetSuccess(string name)
        {
            Success = $"Обєкт {name} Оновлено";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
        } 
    }
}
