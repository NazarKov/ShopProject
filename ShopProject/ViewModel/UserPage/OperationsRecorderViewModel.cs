using ShopProject.Helpers;
using ShopProject.Helpers.Navigation;
using ShopProject.Model.Command;
using ShopProject.Model.UserPage;
using ShopProject.UIModel.ObjectOwnerPage;
using ShopProject.UIModel.OperationRecorderPage;
using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.UserPage
{
    internal class OperationsRecorderViewModel : ViewModel<OperationsRecorderViewModel>
    {
        private OperationsRecorderOperationsModel _model;
        private List<OperationRecorder> _operationsRecorders; 
        private List<ObjectOwner> _objectOwners;

        public OperationsRecorderViewModel() 
        {
            _softwareDeviceSettlementOperationsList = new List<OperationRecorder>();
            _model = new OperationsRecorderOperationsModel();
            _operationsRecorders = new List<OperationRecorder>();
            SetFieldPage();
            _objectOwners = new List<ObjectOwner>();
        }

        private List<OperationRecorder> _softwareDeviceSettlementOperationsList;
        public List<OperationRecorder> SoftwareDeviceSettlementOperationsList
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

        private void SetFieldPage()
        { 
            SetFieldDataListView();
        }

        private void SetFieldDataListView()
        { 
            Task t = Task.Run(async () => { 
                _operationsRecorders.Clear();
                _operationsRecorders = await _model.GetAllOperationsRecorderOperationsUser();
                _objectOwners = await _model.GetObjectOwners(); 
            });
            t.ContinueWith(t => { 
                if (_operationsRecorders != null)
                {
                    SoftwareDeviceSettlementOperationsList.Clear();
                    SoftwareDeviceSettlementOperationsList = _operationsRecorders;

                }
            });
        }

        public ICommand SearchCommand { get => new DelegateParameterCommand(SearchOperationRecorder, CanRegister); }

        private void SearchOperationRecorder(object parameter)
        { 
            Task t = Task.Run(async () => { 
                _operationsRecorders.Clear();
                if (parameter.ToString() != null && parameter.ToString() != "" && parameter.ToString() != " " && parameter.ToString()!=string.Empty)
                { 
                    _operationsRecorders = await _model.Search(parameter.ToString());
                }
                else
                {

                    _operationsRecorders = await _model.GetAllOperationsRecorderOperationsUser();
                }

            });
            t.ContinueWith(t => {
                if (_operationsRecorders != null)
                {
                    SoftwareDeviceSettlementOperationsList.Clear();
                    SoftwareDeviceSettlementOperationsList = _operationsRecorders;
                }
            });
        }

        public ICommand OpenWorkShifMenuCommand { get => new DelegateParameterCommand(OpenWorkShiftMenu, CanRegister); }
        private void OpenWorkShiftMenu(object parameter)
        {
            var item = SoftwareDeviceSettlementOperationsList.ElementAt((int)parameter);
            Session.WorkingShiftStatus.OperationRecorder = item;
            Session.WorkingShiftStatus.ObjectOwner = _objectOwners.FirstOrDefault(o => o.ID == item.ObjectOwner.ID);
            MediatorService.ExecuteEvent(NavigationButton.RedirectToWorkShiftMenu.ToString());
        }
        private bool CanRegister(object parameter) => true;
    }
}
