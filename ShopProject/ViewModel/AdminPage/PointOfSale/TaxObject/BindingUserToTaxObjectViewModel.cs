using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.TaxObject;
using ShopProject.Model.UI.User;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject.Interface;
using ShopProject.Services.Modules.Domain.User.Interface;
using ShopProject.Services.Modules.Mapping.TaxObject;
using ShopProject.Services.Modules.Mapping.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.PointOfSale.TaxObject
{
    internal class BindingUserToTaxObjectViewModel: ViewModel<BindingUserToTaxObjectViewModel>, IViewModelLoadResourse, IСontrolView
    {
        private ICommand _bindingUserToTaxObjectCommand;
        private ICommand _exitWindowCommand;

        private ITaxObjectService _taxObjectService;
        private IUserService _userService;

        public BindingUserToTaxObjectViewModel(ITaxObjectService taxObjectService, IUserService userService)
        {
            _taxObjectService = taxObjectService;
            _userService = userService;
            _error = string.Empty;
            _success = string.Empty;
            _users = new List<UserSelectItemModel>();
            _taxObject = new TaxObjectModel();

            _bindingUserToTaxObjectCommand = CreateCommandAsync(BindingUserToTaxObject);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });

            _successTextBlockVisibiliti = Visibility.Collapsed;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _taxObjectVisibiliti = Visibility.Collapsed;
        }
        public Action? CloseView { get; set; }

        public async Task LoadResourse()
        {
            await SafeExecuteAsync(SetFiledPage);
        }

        private TaxObjectModel _taxObject;
        public TaxObjectModel TaxObject
        {
            get { return _taxObject; }
            set { _taxObject = value; OnPropertyChanged(nameof(TaxObject)); }
        }

        private List<UserSelectItemModel> _users;
        public List<UserSelectItemModel> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged(nameof(Users)); }
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
        private Visibility _taxObjectVisibiliti;
        public Visibility TaxObjectVisibiliti
        {
            get { return _taxObjectVisibiliti; }
            set { _taxObjectVisibiliti = value; OnPropertyChanged(nameof(TaxObjectVisibiliti)); }
        }

        private async Task SetFiledPage()
        {
            var result = await _userService.GetPageColumn(0, 100, ShopProject.Model.Enum.TypeStatusUser.AvailableElectronicKey);
            if (result.IsSuccess)
            {
                Users = new List<UserSelectItemModel>(result.Data.Data.ToUserSelectItemModel());
            }
            else
            {
                SetError("Невдалося завантажити касові апарати");
            }

            var taxObject = _taxObjectService.GetBindingTaxObjectOnSession();
            TaxObject = taxObject.ToTaxObjectModel();
        }

        public ICommand BindingUserToTaxObjectCommand => _bindingUserToTaxObjectCommand;
        public async Task BindingUserToTaxObject()
        {
            var result = await _taxObjectService.AddBindingUserToTaxObject(TaxObject.ID, Users.Where(i => i.IsActive == true).ToUserModel());
            if (result.IsSuccess)
            {
                SetSuccess("Користувачі добавлені");
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Обєкт власноті", "Корисутвачі добавлені")));
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
        private void SetSuccess(string messege)
        {
            Success = messege;
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
        }
    }
}
