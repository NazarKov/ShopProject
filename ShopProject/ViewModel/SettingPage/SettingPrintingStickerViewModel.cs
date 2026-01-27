using ShopProject.Helpers;
using ShopProject.Helpers.Command;
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
    internal class SettingPrintingStickerViewModel : ViewModel<SettingPrintingStickerViewModel>
    {
        private ICommand _saveSettingCommand;
        private ICommand _printTestCheckCommand;
        private SettingPrintingStickerModel _model;
        public SettingPrintingStickerViewModel()
        {
            _model = new SettingPrintingStickerModel();

            _saveSettingCommand = new DelegateCommand(SaveSetting);
            //_printTestCheckCommand = new DelegateCommand(() => { _model.PrintTest(); });

            _model = new SettingPrintingStickerModel();

            _printer = new List<string>(); 
            _selectedPrinter = string.Empty; 

            SetFieldTextBox();
        }

        private void SetFieldTextBox()
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                _printer.Add(printer);
            }

            var json = AppSettingsManager.GetParameterFiles("PrinterSticker").ToString();
            if (json != null)
            {
                var setting = PrinterStickerSetting.Deserialize(json);
                if (setting != null)
                {  
                    SelectedPrinter = setting.Printer;
                }
            }
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

        public ICommand SaveSettingCommand => _saveSettingCommand;

        private void SaveSetting()
        {
            var json = AppSettingsManager.GetParameterFiles("PrinterSticker").ToString();
            if (json != null)
            {
                var setting = PrinterStickerSetting.Deserialize(json);
                if(setting == null)
                {
                    setting = new PrinterStickerSetting();
                }
                setting.Printer = SelectedPrinter;

                AppSettingsManager.SetParameterFile("PrinterSticker", setting.Serialize());
            }
            MessageBox.Show("Дані збережено", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public ICommand PrintTestCheck => _printTestCheckCommand;
    }
}
