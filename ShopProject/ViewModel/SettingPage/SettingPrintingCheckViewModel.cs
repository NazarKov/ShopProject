using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.SettingPage;
using ShopProject.UIModel.SettingPage;
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
        private ICommand _printTestCheckCommand;

        public SettingPrintingCheckViewModel()
        {
            _saveSettingCommand = new DelegateCommand(SaveSetting);
            _printTestCheckCommand = new DelegateCommand(() => { _model.PrintTest(); });

            _model = new SettingPrintingCheckModel();

            _printer = new List<string>();
            _width = 302;
            _slcale = 0.65;
            _selectedPrinter = string.Empty; 

            SetFieldTextBox();
        }
        private void SetFieldTextBox()
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                _printer.Add(printer);
            }

            var json = AppSettingsManager.GetParameterFiles("PrinterCheck").ToString();
            if (json != null) 
            {
                var setting = PrinterFiscalChekSetting.Deserialize(json);
                if (setting != null) 
                {
                    Width = setting.Width;
                    Slcale = setting.Slcale;
                    SelectedPrinter = setting.Printer;
                    SizeQrCode = setting.SizeQrCode;
                } 
            }
        } 
        private List<string> _printer;
        public List<string> Printer
        {
            get { return _printer; }
            set { _printer = value;OnPropertyChanged(nameof(Printer)); }
        }
        private string _selectedPrinter;
        public string SelectedPrinter
        {
            get { return _selectedPrinter; }
            set { _selectedPrinter = value; OnPropertyChanged(nameof(SelectedPrinter)); }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged(nameof(Width)); }
        }
        private double _slcale;
        public double Slcale
        {
            get { return _slcale; }
            set { _slcale = value; OnPropertyChanged(nameof(Width)); }
        }

        private double _sizeQrCode;
        public double SizeQrCode
        {
            get { return _sizeQrCode; }
            set { _sizeQrCode = value; OnPropertyChanged(nameof(SizeQrCode)); }
        }

        public ICommand SaveSettingCommand => _saveSettingCommand;

        private void SaveSetting()
        {
            var json = AppSettingsManager.GetParameterFiles("PrinterCheck").ToString();
            if (json != null)
            {
                var setting = PrinterFiscalChekSetting.Deserialize(json);
                if (setting != null)
                {

                    setting.Width = Width;
                    setting.Slcale = Slcale;
                    setting.Printer = SelectedPrinter;
                    setting.SizeQrCode = SizeQrCode;

                    AppSettingsManager.SetParameterFile("PrinterCheck", setting.Serialize());
                }
            }
            MessageBox.Show("Дані збережено", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public ICommand PrintTestCheck => _printTestCheckCommand;

    }
}
