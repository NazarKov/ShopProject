using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Core.Mvvm.CompositionRoot.Interface;
using ShopProject.Model.Domain.Setting;
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Modules.Setting.Interface;
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
    internal class SettingPrintingCheckViewModel : ViewModel<SettingPrintingCheckViewModel> , IViewModelLoadResourse
    { 
        private ICommand _saveSettingCommand;
        private ICommand _printTestCheckCommand;

        private ISettingService _settingService;
        private IPrintingFiscalCheckService _printingFiscalCheckService;

        public SettingPrintingCheckViewModel(ISettingService settingService , IPrintingFiscalCheckService printingFiscalCheckService)
        {
            _printingFiscalCheckService = printingFiscalCheckService;
            _settingService = settingService;
            _saveSettingCommand = CreateCommand(SaveSetting);
            _printTestCheckCommand = CreateCommand(PrintTest);

            _printer = new List<string>();
            _width = 302;
            _slcale = 0.65;
            _selectedPrinter = string.Empty; 
        }

        public Task LoadResourse()
        {
            SafeExecute(SetFieldTextBox);
            return Task.CompletedTask;
        }
        private void SetFieldTextBox()
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                _printer.Add(printer);
            }

            var setting = _settingService.GetSetting<PrinterFiscalChekSetting>();

            if(setting == null)
            {
                setting = new PrinterFiscalChekSetting();
                return;
            }

            Width = setting.Width;
            Slcale = setting.Slcale;
            SelectedPrinter = setting.Printer;
            SizeQrCode = setting.SizeQrCode;
        }
        private List<string> _printer;
        public List<string> Printer
        {
            get { return _printer; }
            set { _printer = value; OnPropertyChanged(nameof(Printer)); }
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
            _settingService.SetSetting<PrinterFiscalChekSetting>(new PrinterFiscalChekSetting()
            {
                Printer = SelectedPrinter,
                SizeQrCode = SizeQrCode,
                Slcale = Slcale,
                Width = Width,
            });
            MessageBox.Show("Дані збережено", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
        } 
        public ICommand PrintTestCheck => _printTestCheckCommand;
        private void PrintTest()
        {
            _printingFiscalCheckService.PrintCheck(new Resource.template.TemplatePrintingCheck() 
            {
                
            });
        }

    }
}
