using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Setting;
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingPrintingStickerViewModel : ViewModel<SettingPrintingStickerViewModel> , IViewModelLoadResourse
    {
        private ICommand _saveSettingCommand;
        private ICommand _printTestCheckCommand;

        private ISettingService _settingService;
        private IPrintingStikerService _printingStikerService;

        public SettingPrintingStickerViewModel(ISettingService settingService,IPrintingStikerService printingStikerService)
        {
            _settingService  = settingService;
            _printingStikerService = printingStikerService;

            _saveSettingCommand = CreateCommand(SaveSetting);
            _printTestCheckCommand = CreateCommand(PrintTest);

            _printer = new List<string>();
            _selectedPrinter = string.Empty;
            _nameCompany = string.Empty;
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

            var setting = _settingService.GetSetting<PrinterStickerSetting>();
            if(setting == null)
            {
                setting = new PrinterStickerSetting();
            }
            NameCompany = setting.NameCompany;
            SelectedPrinter = setting.Printer;  
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

        private string _nameCompany;
        public string NameCompany
        {
            get { return _nameCompany; }
            set { _nameCompany = value; OnPropertyChanged(nameof(NameCompany)); }
        }

        public ICommand SaveSettingCommand => _saveSettingCommand;

        private void SaveSetting()
        {
            _settingService.SetSetting<PrinterStickerSetting>(new PrinterStickerSetting() { Printer = SelectedPrinter ,NameCompany = NameCompany });
            MessageBox.Show("Дані збережено", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
        } 
        public ICommand PrintTestCheck => _printTestCheckCommand;
        private void PrintTest()
        {
            _printingStikerService.SetShowTextInImage(true,true,true,true);
            _printingStikerService.CreateBarCode("ТОВ Тест", "Товар 1", "Опис товар1", "1234567891231");
            _printingStikerService.Print();
        }
    }
}
