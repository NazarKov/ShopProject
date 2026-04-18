using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.ProductUnit;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.ProductUnit.Interface;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.Storage.ProductUnit
{
    internal class CreateProductUnitViewModel : ViewModel<CreateProductUnitViewModel> , IСontrolView
    { 

        private readonly ICommand _createProductUnitCommand;
        private readonly ICommand _exitWindowCommand;
        private IProductUnitServiсe _productUnitServiсe;

        public CreateProductUnitViewModel(IProductUnitServiсe productUnitServiсe)
        {
            _productUnitServiсe = productUnitServiсe; 
            _createProductUnitCommand = CreateCommandAsync(CreateProductUnit,SetError);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });
            
            _unit = new ProductUnitModel();
            _error = string.Empty;
            _success = string.Empty;
            _isEnableSaveButton = true;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed;
             
        } 

        public Action? CloseView {  get; set; }

        private ProductUnitModel _unit;
        public ProductUnitModel Unit
        {
            get { return _unit; }
            set { _unit = value; }
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


        public ICommand CreateProductUnitCommand => _createProductUnitCommand;
        private async Task CreateProductUnit()
        {
            Unit.Status = ShopProject.Model.Enum.TypeStatusUnit.Favorite;
            await _productUnitServiсe.Add(Unit.ToProductUnit());

            SetSuccess();
            await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Одиниці", "Одиниця успішно створена в базі даних")));
            await MediatorService.ExecuteEventAsync("ReloadUnitsGriedView");
        }

        private void SetError(string error)
        {
            IsEnableSaveButton = true;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            Error = error;
            ErrorTextBlockVisibiliti = Visibility.Visible;
        }
        private void SetSuccess()
        {
            Success = "Одиниця добавлена";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
            IsEnableSaveButton = true;
        }

        public ICommand ExitWindowCommand => _exitWindowCommand;
    }
}
