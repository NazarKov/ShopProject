using ShopProject.Model.Command;
using ShopProject.Model.SettingPage;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingPrintingCheckViewModel : ViewModel<SettingPrintingCheckViewModel>
    {
        SettingPrintingCheckModel _model;

        private ICommand _saveSettingCommand;
        private ICommand _printTestCheck;

        public SettingPrintingCheckViewModel()
        {
            _saveSettingCommand = new DelegateCommand(SaveSetting);
            _printTestCheck = new DelegateCommand(() => { _model.PrintTest(); });

            _model = new SettingPrintingCheckModel();
            SetFieldTextBox();
        }
        private void SetFieldTextBox()
        {
            List<string> setting = _model.Get();
            NameShop = setting[0];
            NameSeller = setting[1];
            NameFop = setting[2];
            Region = setting[3];
            District = setting[4];
            City = setting[5];
            Street = setting[6];
            House = setting[7];
            Printer = PrinterSettings.InstalledPrinters.Cast<string>().ToList();
            SelectedPrinter = setting[8];
        }

        private string _nameShop;
        public string NameShop
        {
            get { return _nameShop; }
            set { _nameShop = value; OnPropertyChanged("NameShop"); }
        }

        private string _nameSeller;
        public string NameSeller
        {
            get { return _nameSeller; }
            set { _nameSeller = value; OnPropertyChanged("NameSeller"); }
        }
        private string _nameFop;
        public string NameFop
        {
            get { return _nameFop; }
            set { _nameFop = value; OnPropertyChanged("NameFop"); }
        }
        private string _region;
        public string Region
        {
            get { return _region; }
            set { _region = value; OnPropertyChanged("Region"); }
        }
        private string _district;
        public string District
        {
            get { return _district; }
            set { _district = value; OnPropertyChanged("District"); }
        }
        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged("City"); }
        }
        private string _street;
        public string Street
        {
            get { return _street; }
            set { _street = value; OnPropertyChanged("Street"); }
        }
        private string _house;
        public string House
        {
            get { return _house; }
            set { _house = value; OnPropertyChanged("House"); }
        }
        private List<string> _printer;
        public List<string> Printer
        {
            get { return _printer; }
            set { _printer = value;OnPropertyChanged("Printer"); }
        }
        private string _selectedPrinter;
        public string SelectedPrinter
        {
            get { return _selectedPrinter; }
            set { _selectedPrinter = value; OnPropertyChanged("SelectedPrinter"); }
        }

        public ICommand SaveSettingCommand => _saveSettingCommand;

        private void SaveSetting()
        {
            _model.Save(NameShop, NameSeller, NameFop, Region, District, City, Street, House, SelectedPrinter);
            MessageBox.Show("Дані збережено", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public ICommand PrintTestCheck => _printTestCheck;

    }
}
