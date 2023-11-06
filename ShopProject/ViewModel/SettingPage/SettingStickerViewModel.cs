using ShopProject.Model.Command;
using ShopProject.Model.SettingPage;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingStickerViewModel : ViewModel<SettingStickerViewModel>
    {
        private SettingStickerModel _model;

        private ICommand _saveSettingsCommand;
        private ICommand _printTestPageCommand;

        public SettingStickerViewModel()
        {
            _model = new SettingStickerModel();

            _saveSettingsCommand = new DelegateCommand(() => { _model.Save(SelectPrinter); });
            _printTestPageCommand = new DelegateCommand(() => { _model.PrintTest(); });
            
            SetFieldPage();
        }
        private void SetFieldPage()
        {
            Printers = PrinterSettings.InstalledPrinters.Cast<string>().ToList();
            SelectPrinter = _model.Get();
        }

        private List<string> _printers;
        public List<string> Printers
        {
            get { return _printers; }
            set { _printers = value; OnPropertyChanged("Printers"); }
        }

        private string _selectPrinter;
        public string SelectPrinter
        {
            get { return _selectPrinter; }
            set { _selectPrinter = value; OnPropertyChanged("SelectPrinter"); }
        }
        public ICommand SaveSettingsCommand => _saveSettingsCommand;
        public ICommand PrintTestPageCommand => _printTestPageCommand;


    }
}