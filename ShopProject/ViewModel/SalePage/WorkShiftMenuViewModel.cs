using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.SalePage;
using ShopProject.Views.SalePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.SalePage
{
    internal class WorkShiftMenuViewModel : ViewModel<WorkShiftMenuViewModel>
    {
        private WorkShiftMenuModel _model;

        private ICommand _openShiftCommand;
        private ICommand _closeShiftCommand;

        private ICommand _updateSize;

        public WorkShiftMenuViewModel ()
        {
            _model = new WorkShiftMenuModel ();

            _openShiftCommand = new DelegateCommand(OpenShift);
            _closeShiftCommand = new DelegateCommand(CloseShift);
            _updateSize = new DelegateCommand(UpdateSizes);

            _statusShift = string.Empty;
            _statusColor = string.Empty;

            setFieldPage();
        }

        private string _statusShift;
        public string StatusShift
        {
            get { return _statusShift; }
            set { _statusShift = value; OnPropertyChanged("StatusShift"); }
        }
        private string _statusColor;
        public string StatusColor
        {
            get { return _statusColor; }
            set { _statusColor = value; OnPropertyChanged("StatusColor"); }
        }
        private string _fnNumber;
        public string FNumber
        {
            get { return _fnNumber; }
            set { _fnNumber = value;OnPropertyChanged("FNumber"); }
        }
        private string _economicUnit;
        public string EconomicUnit
        {
            get { return _economicUnit; }
            set { _economicUnit = value; OnPropertyChanged("EconomicUnit"); }
        }
        private string _seller;
        public string Seller
        {
            get { return _seller; }
            set { _seller = value; OnPropertyChanged("Seller"); }
        }
        private string _statusOnline;
        public string StatusOnline
        {
            get { return _statusOnline; }
            set { _statusOnline = value; OnPropertyChanged("StatusOnline"); }
        }
        private Page _salePage;
        public Page SalePage
        {
            get { return _salePage; }
            set { _salePage = value;OnPropertyChanged("SalePage"); }
        }

        private int _widght;
        public int Widght
        {
            get { return _widght; }
            set { _widght = value; OnPropertyChanged("Widght"); }
        }


        private int _height;
        public int Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged("Height"); }
        }

        public ICommand UpdateSize => _updateSize;

        private void UpdateSizes()
        {
            Widght = (int)Application.Current.MainWindow.ActualWidth;
            Height = (int)Application.Current.MainWindow.ActualHeight;
        }


        private void setFieldPage()
        {
            SalePage = new SaleMenu();
            StatusShift = AppSettingsManager.GetParameterFiles("StatusWorkShift").ToString();
            if (StatusShift == "Зміна відкрита")
            {
                StatusColor = "Green";
            }
            else
            {
                StatusColor = "Red";
            }
            FNumber = AppSettingsManager.GetParameterFiles("FiscalNumberRRO").ToString();
            EconomicUnit = AppSettingsManager.GetParameterFiles("NameShop").ToString();
            Seller = AppSettingsManager.GetParameterFiles("NameSeller").ToString();
            StatusOnline = AppSettingsManager.GetParameterFiles("StatusWorkShiftTime").ToString();
        }


        public ICommand OpenShiftCommand => _openShiftCommand;
        private void OpenShift()
        {
            Operation operation = new Operation
            {
                dataPacketIdentifier = 1,
                typeRRO = 0,
                fiscalNumberRRO = AppSettingsManager.GetParameterFiles("FiscalNumberRRO").ToString(),
                taxNumber = AppSettingsManager.GetParameterFiles("TaxNumber").ToString(),
                factoryNumberRRO = "v1",
                typeOperation = 108,
                createdAt = DateTime.Now,
                numberPayment = "0",
                mac = _model.GetMac(false),
            };

            _model.OpenChange(operation, true);
            StatusShift = "Зміна відкрита";
            StatusColor = "Green";
            AppSettingsManager.SetParameterFile("StatusWorkShift", StatusShift);
            StatusOnline = "з " + DateTime.Now.ToString("g");
            AppSettingsManager.SetParameterFile("StatusWorkShiftTime", StatusOnline);
        }

        public ICommand CloseShiftCommand => _closeShiftCommand;
        private void CloseShift()
        {
            _model.CloseChange(new Operation
            {
                dataPacketIdentifier = 1,
                typeRRO = 0,
                fiscalNumberRRO = AppSettingsManager.GetParameterFiles("FiscalNumberRRO").ToString(),
                taxNumber = AppSettingsManager.GetParameterFiles("TaxNumber").ToString(),
                factoryNumberRRO = "v1",
                mac = _model.GetMac(true),
                createdAt = DateTime.Now,
                numberPayment = _model.GetLocalNumber(),
                numberOfSalesReceipts = Convert.ToDecimal(_model.GetLocalNumber()),

            });

            StatusShift = "Зміна закрита";
            StatusColor = "Red";
            AppSettingsManager.SetParameterFile("StatusWorkShift", StatusShift);
            StatusOnline = string.Empty;
            AppSettingsManager.SetParameterFile("StatusWorkShiftTime", StatusOnline);

        }

    }
}
