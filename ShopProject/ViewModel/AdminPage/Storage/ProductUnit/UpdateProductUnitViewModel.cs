using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Core.Mvvm.CompositionRoot.Interface;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Core.Mvvm.Mediator.Notifications;
using ShopProject.Core.Mvvm.Service;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.ProductUnit;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.ProductUnit.Interface; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage.ProductUnitPage
{
    internal class UpdateProductUnitViewModel : ViewModel<UpdateProductUnitViewModel> ,IViewModelLoadResourse , IСontrolView
    { 

        private readonly ICommand _updateProductUnitCommand;
        private readonly ICommand _exitWindowCommand;
        private IProductUnitServiсe _productUnitServiсe;
         
        public UpdateProductUnitViewModel(IProductUnitServiсe productUnitServiсe)
        {
            _productUnitServiсe = productUnitServiсe; 
            _updateProductUnitCommand = CreateCommandAsync(UpdateProductUnit);
            _statusUnit = new List<string>();
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });

            _unit = new ProductUnitModel();
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

        public ICommand ExitWindowCommand => _exitWindowCommand;

        public Action? CloseView { get; set; }

        private void SetFieldWindow()
        {
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed;
            Unit = _productUnitServiсe.GetUnitFromSession().ToProductUnitModel();

            SetFieldComboBoxStatusUnit();
            if (_unit != null)
            {
                SelectStatusUnit = Enum.GetValues<TypeStatusUnit>().ToList().IndexOf(_unit.Status);
            }

        }

        private void SetFieldComboBoxStatusUnit()
        {
            StatusUnit = new List<string>(ProductUnitStatusModel.GetStatus());  
        }

        private ProductUnitModel _unit;
        public ProductUnitModel Unit
        {
            get { return _unit; }
            set { _unit = value; OnPropertyChanged(nameof(Unit)); }
        }

        private List<string> _statusUnit;
        public List<string> StatusUnit
        {
            get { return _statusUnit; }
            set { _statusUnit = value; OnPropertyChanged(nameof(StatusUnit)); }
        }

        private int _selectStatusUnit;
        public int SelectStatusUnit
        {
            get { return _selectStatusUnit; }
            set { _selectStatusUnit = value; OnPropertyChanged(nameof(SelectStatusUnit)); }
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

        public ICommand UpdateProductUnitCommand => _updateProductUnitCommand;
        private async Task UpdateProductUnit()
        {
            Unit.Status = Enum.GetValues<TypeStatusUnit>().ElementAt(SelectStatusUnit);
            if(await _productUnitServiсe.Update(Unit.ToProductUnit()))
            {
                SetSuccess();
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Одиниці", "Одиниця успішно редагована в базі даних")));
                await MediatorService.ExecuteEventAsync(NavigationButton.ReloadProduct.ToString());
            } 
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
            Success = "Одиниця успішно редагована";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
            IsEnableSaveButton = true;
        }
    }
}
