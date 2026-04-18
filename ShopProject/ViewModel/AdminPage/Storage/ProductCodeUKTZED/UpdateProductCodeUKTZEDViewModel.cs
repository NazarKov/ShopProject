using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Domain.PorductCodeUKTZED;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.ProductCodeUKTZED;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.ProductCodeUKTZED.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage.ProductCodeUKTZEDPage
{
    internal class UpdateProductCodeUKTZEDViewModel : ViewModel<UpdateProductCodeUKTZEDViewModel> ,IViewModelLoadResourse, IСontrolView
    { 

        private readonly ICommand _updateProductCodeUKTZEDCommand;
        private readonly ICommand _exitWindowCommand;
        private IProductCodeUKTZEDServiсe _productCodeUKTZEDServiсe;

        public UpdateProductCodeUKTZEDViewModel(IProductCodeUKTZEDServiсe productCodeUKTZEDServiсe)
        {
            _productCodeUKTZEDServiсe = productCodeUKTZEDServiсe;
            _productCodeUKTZED = new ProductCodeUKTZEDModel();
            _statusCodeUKTZED = new List<string>();

            _updateProductCodeUKTZEDCommand = CreateCommandAsync(UpdateProductCodeUKTZED, SetError);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });

            _error = string.Empty;
            _success = string.Empty;
            _isEnableSaveButton = true;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed;
        } 

        public Task LoadResourse()
        {
            SafeExecute(SetFieldWindow);

            return Task.CompletedTask;
        }

        public Action? CloseView { get; set; }

        private void SetFieldWindow()
        {
            var code = _productCodeUKTZEDServiсe.GetProductCodeUKTZEDFromSession(); 

            SetFieldComboBoxStatusCodeUKTZED(); 
            if (code != null)
            {

                ProductCodeUKTZED = code.ToProductCodeUKTZEDModel();
                SelectStatusCodeUTKZED = Enum.GetValues<TypeStatusCodeUKTZED>().ToList().IndexOf(code.Status);
            }

        }

        private void SetFieldComboBoxStatusCodeUKTZED()
        {
            StatusCodeUKTZED = new List<string>(ProductCodeUKTZEDStatusModel.GetStatus());
        } 
        private ProductCodeUKTZEDModel _productCodeUKTZED;
        public ProductCodeUKTZEDModel ProductCodeUKTZED
        {
            get { return _productCodeUKTZED; }
            set { _productCodeUKTZED = value;OnPropertyChanged(nameof(ProductCodeUKTZED)); }
        }  
        private List<string> _statusCodeUKTZED;
        public List<string> StatusCodeUKTZED
        {
            get { return _statusCodeUKTZED; }
            set { _statusCodeUKTZED = value; OnPropertyChanged(nameof(StatusCodeUKTZED)); }
        }

        private int _selectStatusCodeUKTZED;
        public int SelectStatusCodeUTKZED
        {
            get { return _selectStatusCodeUKTZED; }
            set { _selectStatusCodeUKTZED = value; OnPropertyChanged(nameof(SelectStatusCodeUTKZED)); }
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
        public ICommand UpdateProductCodeUKTZEDCommand => _updateProductCodeUKTZEDCommand;
        private async Task UpdateProductCodeUKTZED()
        {
            ProductCodeUKTZED.Status = Enum.GetValues<TypeStatusCodeUKTZED>().ElementAt(SelectStatusCodeUTKZED);
            await _productCodeUKTZEDServiсe.Update(ProductCodeUKTZED.ToProductCodeUKTZED());

            SetSuccess(ProductCodeUKTZED.Code);
            await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Код", "Код успішно редаговано в базі даних")));
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
            Success = "Код " + number + " редаговано";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
            IsEnableSaveButton = true;
        }

        public ICommand ExitWindowCommand => _exitWindowCommand;
         
    }
}
