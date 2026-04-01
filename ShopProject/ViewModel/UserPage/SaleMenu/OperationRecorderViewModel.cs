using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.CompositionRoot.Interface;
using ShopProject.Core.Mvvm.Service;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.ObjectOwner;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Services.Modules.Mapping;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.ObjectOwner.Interface;
using ShopProject.Services.Modules.ModelService.OperationRecorder.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.UserPage.SaleMenu
{
    internal class OperationRecorderViewModel : ViewModel<OperationRecorderViewModel> , IViewModelLoadResourse
    { 
        private List<OperationRecorderModel> _operationsRecorders;
        private List<ObjectOwnerModel> _objectOwners;

        private IOperationRecorderServise _operationRecorderServise;
        private IObjectOwnerService _objectOwnerService;

        public OperationRecorderViewModel(IOperationRecorderServise operationRecorderServise,IObjectOwnerService objectOwnerService)
        {
            _operationRecorderServise = operationRecorderServise;
            _objectOwnerService = objectOwnerService;
            _softwareDeviceSettlementOperationsList = new List<OperationRecorderModel>(); 
            _operationsRecorders = new List<OperationRecorderModel>(); 
            _objectOwners = new List<ObjectOwnerModel>();
        }
        public async Task LoadResourse()
        {
           await SafeExecuteAsync(SetFieldPage);
        }
        private List<OperationRecorderModel> _softwareDeviceSettlementOperationsList;
        public List<OperationRecorderModel> SoftwareDeviceSettlementOperationsList
        {
            get { return _softwareDeviceSettlementOperationsList; }
            set { _softwareDeviceSettlementOperationsList = value; OnPropertyChanged(nameof(SoftwareDeviceSettlementOperationsList)); }
        }
        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        private async Task SetFieldPage()
        {
            await SetFieldDataListView();
        }

        private async Task SetFieldDataListView()
        { 
            SoftwareDeviceSettlementOperationsList = new List<OperationRecorderModel>((await _operationRecorderServise.GetAllOperationsRecorderOperationsUser()).ToOperationRecorderModel().ToList());
            _objectOwners = new List<ObjectOwnerModel>((await _objectOwnerService.GetObjectOwners()).ToObjectOwnerModel().ToList()); 
        }

        public ICommand SearchCommand { get => CreateCommandParameter<object>(SearchOperationRecorder); }

        private void SearchOperationRecorder(object parameter)
        {
            Task t = Task.Run(async () =>
            {
                _operationsRecorders.Clear();
                if (parameter.ToString() != null && parameter.ToString() != "" && parameter.ToString() != " " && parameter.ToString() != string.Empty)
                {
                    //_operationsRecorders = await _model.Search(parameter.ToString());
                }
                else
                {

                    //_operationsRecorders = await _model.GetAllOperationsRecorderOperationsUser();
                }

            });
            t.ContinueWith(t =>
            {
                if (_operationsRecorders != null)
                {
                    SoftwareDeviceSettlementOperationsList.Clear();
                    SoftwareDeviceSettlementOperationsList = _operationsRecorders;
                }
            });
        }

        public ICommand OpenWorkShifMenuCommand { get => CreateCommandParameter<object>(OpenWorkShiftMenu); }
        private void OpenWorkShiftMenu(object parameter)
        {
            if (parameter != null)
            {
                var item = SoftwareDeviceSettlementOperationsList.ElementAt((int)parameter);
                _operationRecorderServise.SetOperationRecorderOnWorkingShiftStatusInSession(item.ToOperationRecorder());
                _objectOwnerService.SetObjectOwnerOnWorkingShiftStatusInSession(_objectOwners.First(o => o.ID == item?.ObjectOwner?.ID).ToObjectOwner()); 
                MediatorService.ExecuteNavigation(NavigationButton.RedirectToWorkShiftMenuPage);
            }
        } 
    }
}
