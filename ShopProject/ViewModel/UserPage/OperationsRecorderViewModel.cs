using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.UserPage;
using ShopProjectSQLDataBase.Entities;
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
        private List<OperationsRecorderEntity> _operationsRecorders;


        public OperationsRecorderViewModel() 
        {
            _softwareDeviceSettlementOperationsList = new List<OperationsRecorderEntity>();
            _model = new OperationsRecorderOperationsModel();
            _operationsRecorders = new List<OperationsRecorderEntity>();
            SetFieldPage();
        }


        private List<OperationsRecorderEntity> _softwareDeviceSettlementOperationsList;
        public List<OperationsRecorderEntity> SoftwareDeviceSettlementOperationsList
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
            Session.FocusDevices = SoftwareDeviceSettlementOperationsList.ElementAt((int)parameter);
            Mediator.Notify("RedirectToWorkShiftMenu", "");

        }
        private bool CanRegister(object parameter) => true;
    }
}
