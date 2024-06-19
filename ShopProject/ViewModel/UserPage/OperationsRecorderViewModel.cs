using LocateWindow;
using NPOI.SS.Formula.Functions;
using ShopProject.DataBase.Entities;
using ShopProject.Helpers;
using ShopProject.Model.Command;
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

        private ICommand _openWorkShiftMenu;

        public OperationsRecorderViewModel() 
        {
            _softwareDeviceSettlementOperationsList = new List<OperationsRecorderEntiti>();
 
            setFieldPage();

        }


        private List<OperationsRecorderEntiti> _softwareDeviceSettlementOperationsList;
        public List<OperationsRecorderEntiti> SoftwareDeviceSettlementOperationsList
        {
            get { return _softwareDeviceSettlementOperationsList; }
            set {  _softwareDeviceSettlementOperationsList = value; OnPropertyChanged("SoftwareDeviceSettlementOperationsList"); }
        }
        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }

        private void setFieldPage()
        {
            var items = Session.Devices;
            if (items != null)
            {
                SoftwareDeviceSettlementOperationsList = items;
            }
        }


        public ICommand OpenWorkShifMenuCommand { get => new DelegateParameterCommand(OpenWorkShiftMenu, CanRegister); }
        private void OpenWorkShiftMenu(object parameter)
        {
            Session.FocusDevices = SoftwareDeviceSettlementOperationsList.ElementAt((int)parameter);
            Mediator.Notify("OpenWorkShiftMenu", "");

        }
        private bool CanRegister(object parameter) => true;
    }
}
