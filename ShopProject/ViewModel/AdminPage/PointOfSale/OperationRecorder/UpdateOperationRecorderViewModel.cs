using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Domain.OperationRecorder;
using ShopProject.Services.Modules.Domain.OperationRecorder.Interface;
using ShopProject.Services.Modules.Mapping.OperationRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.PointOfSale.OperationRecorder
{
    internal class UpdateOperationRecorderViewModel : ViewModel<UpdateOperationRecorderViewModel>, IСontrolView, IViewModelLoadResourse
    {
        private ICommand _updateOperaionRecorderCommand;
        private ICommand _exitWindowCommand;
        private IOperationRecorderService _operationRecorderService;

        public UpdateOperationRecorderViewModel(IOperationRecorderService operationRecorderService)
        {
            _operationRecorderService = operationRecorderService;
            _error = string.Empty;
            _success = string.Empty;
            _operationRecorderModel = new OperationRecorderModel();

            _updateOperaionRecorderCommand = CreateCommandAsync(UpdateOperaionRecorder);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); }); 

            _successTextBlockVisibiliti = Visibility.Collapsed;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
        }
        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);
            return Task.CompletedTask;
        }

        public Action? CloseView { get; set; }

        private OperationRecorderModel _operationRecorderModel;
        public OperationRecorderModel OperationRecorderModel
        {
            get { return _operationRecorderModel; }
            set { _operationRecorderModel = value; OnPropertyChanged(nameof(OperationRecorderModel)); }
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
            var item = _operationRecorderService.GetOperationrecorderInSession();
            if (item != null)
            {
                OperationRecorderModel = item.ToOperationRecorderModel();
            }
        }

        public ICommand UpdateOperaionRecorderCommand => _updateOperaionRecorderCommand;

        public async Task UpdateOperaionRecorder()
        {
            var result = await _operationRecorderService.Update(OperationRecorderModel.ToOperationRecorder());
            if (result.IsSuccess)
            {
                SetSuccess(result.Data.Name);
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Каса", "Касовий апарат успішно редаговано")));
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
        public ICommand ExitWindowCommand => _exitWindowCommand;
        private void SetError(string error)
        {
            Error = error;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            ErrorTextBlockVisibiliti = Visibility.Visible;
        }
        private void SetSuccess(string name)
        {
            Success = $"Касовий апарат {name} редаговано";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
        }
    }
}
