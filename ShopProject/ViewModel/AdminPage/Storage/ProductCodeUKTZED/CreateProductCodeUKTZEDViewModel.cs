using ShopProject.Infrastructure.Mvvm;
using ShopProject.Infrastructure.Mvvm.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.UI.ProductCodeUKTZED;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Common.Interface;
using ShopProject.Services.Modules.Domain.ProductCodeUKTZED.Interface;
using ShopProject.Services.Modules.Mapping.ProductCodeUKTZED;
using System;
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
        private IHandlerOperationResultService _handlerOperationResultService;
        public CreateProductCodeUKTZEDViewModel(IProductCodeUKTZEDServiсe productCodeUKTZEDServiсe,IHandlerOperationResultService handlerOperationResultService)
        {
            _productCodeUKTZEDServiсe = productCodeUKTZEDServiсe;
            _handlerOperationResultService = handlerOperationResultService;
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
            var result = await _productCodeUKTZEDServiсe.Add(ProductCodeUKTZED.ToProductCodeUKTZED());
            if (result.IsSuccess)
            {
                SetSuccess(ProductCodeUKTZED.Code);
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Код", "Код успішно створений в базі даних")));
                await MediatorService.ExecuteEventAsync("ReloadCodeUKTEDGriedView");

            }
            else if(result.IsError) 
            {
                SetError(await _handlerOperationResultService.CheckOperaionResultErrorStatus(result,CloseView));
            }
            else
            {
                SetError("Невдалося виконати операцію");
            }
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
