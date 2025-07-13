using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.UserPage;
using ShopProjectDataBase.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.UserPage
{
    internal class OperationsRecorderViewModel : ViewModel<OperationsRecorderViewModel>
    {
        private OperationsRecorderOperationsModel _model;
        private ICommand _openWorkShiftMenu;

        public OperationsRecorderViewModel() 
        {
            _softwareDeviceSettlementOperationsList = new List<OperationsRecorderEntity>();
            _model = new OperationsRecorderOperationsModel();

            setFieldPage();

        }


        private List<OperationsRecorderEntity> _softwareDeviceSettlementOperationsList;
        public List<OperationsRecorderEntity> SoftwareDeviceSettlementOperationsList
        {
            get { return _softwareDeviceSettlementOperationsList; }
            set { _softwareDeviceSettlementOperationsList = value; OnPropertyChanged("SoftwareDeviceSettlementOperationsList"); }
        }
        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }

        private void setFieldPage()
        {
            var items = _model.GetAllOperationsRecorderOperationsUser();
            if (items != null)
            {
                SoftwareDeviceSettlementOperationsList = items;
            }
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
