using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Core.Mvvm.Mediator.Notifications;
using ShopProject.Core.Mvvm.Service;
using ShopProject.Helpers;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.UI.ProductCodeUKTZED;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.ProductCodeUKTZED.Interface;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage.ProductCodeUKTZEDPage
{
    internal class CreateProductCodeUKTZEDViewModel : ViewModel<CreateProductCodeUKTZEDViewModel> , IСontrolView
    { 

        private readonly ICommand _createProductCodeUKTZEDCommand;
        private readonly ICommand _exitWindowCommand;
        private IProductCodeUKTZEDServiсe _productCodeUKTZEDServiсe;
        public CreateProductCodeUKTZEDViewModel(IProductCodeUKTZEDServiсe productCodeUKTZEDServiсe)
        {
            _productCodeUKTZEDServiсe = productCodeUKTZEDServiсe;
            _productCodeUKTZED = new ProductCodeUKTZEDModel();

            _createProductCodeUKTZEDCommand = CreateCommandAsync(CreateProductCodeUKTZED, SetError);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });
            
            _error = string.Empty;
            _success = string.Empty;
            _isEnableSaveButton = true;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed; 
        }
        public Action? CloseView { get; set; }

        private ProductCodeUKTZEDModel _productCodeUKTZED;
        public ProductCodeUKTZEDModel ProductCodeUKTZED
        {
            get { return _productCodeUKTZED; }
            set { _productCodeUKTZED = value;OnPropertyChanged(nameof(ProductCodeUKTZED));}
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

        private bool _isEnableSaveButton;
        public bool IsEnableSaveButton
        {
            get { return _isEnableSaveButton; }
            set { _isEnableSaveButton = value; OnPropertyChanged(nameof(IsEnableSaveButton)); }
        }

        public ICommand CreateProductCodeUKTZEDCommand => _createProductCodeUKTZEDCommand;
        private async Task CreateProductCodeUKTZED()
        {
            ProductCodeUKTZED.Status = ShopProject.Model.Enum.TypeStatusCodeUKTZED.Favorite;
            await _productCodeUKTZEDServiсe.Add(ProductCodeUKTZED.ToProductCodeUKTZED());

            SetSuccess(ProductCodeUKTZED.Code);
            await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Код", "Код успішно створений в базі даних")));
            await MediatorService.ExecuteEventAsync("ReloadCodeUKTEDGriedView");
        }
        private void SetError(string error)
        {
            IsEnableSaveButton = true;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            Error = error;
            ErrorTextBlockVisibiliti = Visibility.Visible;
        }
        private void SetSuccess(string number)
        {
            Success = "Код "+ number + " добавлена";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
            IsEnableSaveButton = true;
        }


        public ICommand ExitWindowCommand => _exitWindowCommand; 
    }
}
