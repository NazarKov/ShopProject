using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Notification; 
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Model.UI.TaxObject;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Domain.OperationRecorder.Interface;
using ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject.Interface;  
using ShopProject.Services.Modules.Mapping.OperationRecorder;
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
    internal class BindingOperationRecorderToTaxObjectViewModel : ViewModel<BindingOperationRecorderToTaxObjectViewModel>, IViewModelLoadResourse, IСontrolView
    {
        private ICommand _bindingOperationRecorderToTaxObjectCommand;
        private ICommand _exitWindowCommand; 

        private ITaxObjectService _taxObjectService;
        private IOperationRecorderService _operationRecorderService;

        public BindingOperationRecorderToTaxObjectViewModel(ITaxObjectService taxObjectService,IOperationRecorderService operationRecorderService)
        {
            _taxObjectService = taxObjectService;
            _operationRecorderService = operationRecorderService;
            _error = string.Empty;
            _success = string.Empty;
            _operationRecorders = new List<OperationRecorderSelectItemModel>();
            _taxObject = new TaxObjectModel();

            _bindingOperationRecorderToTaxObjectCommand = CreateCommandAsync(BindingOperationRecorderToTaxObject);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); }); 

            _successTextBlockVisibiliti = Visibility.Collapsed;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _taxObjectVisibiliti = Visibility.Collapsed;
        }
        public Action? CloseView { get; set; }

        public async Task LoadResourse()
        {
           await  SafeExecuteAsync(SetFiledPage);
        }

        private TaxObjectModel _taxObject;
        public TaxObjectModel TaxObject
        {
            get { return _taxObject; }
            set { _taxObject = value; OnPropertyChanged(nameof(TaxObject)); }
        }

        private List<OperationRecorderSelectItemModel> _operationRecorders;
        public List<OperationRecorderSelectItemModel> OperationRecorders
        {
            get { return _operationRecorders; }
            set { _operationRecorders = value; OnPropertyChanged(nameof(OperationRecorders)); }
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
            var result =await _operationRecorderService.GetPageColumn(0, 100, ShopProject.Model.Enum.TypeStatusOperationRecorder.Open);
            if (result.IsSuccess)
            {
                OperationRecorders = new List<OperationRecorderSelectItemModel>(result.Data.Data.ToOperationRecorderSelectItemModel());
            }
            else
            {
                SetError("Невдалося завантажити касові апарати");
            }

            var taxObject = _taxObjectService.GetBindingTaxObjectOnSession();
            TaxObject = taxObject.ToTaxObjectModel(); 
        }

        public ICommand BindingOperationRecorderToTaxObjectCommand => _bindingOperationRecorderToTaxObjectCommand;
        public async Task BindingOperationRecorderToTaxObject()
        {
            var result = await _taxObjectService.AddBindingOperationRecorderToTaxObject(TaxObject.ID,OperationRecorders.Where(i=>i.IsActive==true).ToOperationRecorderModel());
            if (result.IsSuccess)
            {
                SetSuccess("Каси добавлені");
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Обєкт власноті", "Каси добавлені")));
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
