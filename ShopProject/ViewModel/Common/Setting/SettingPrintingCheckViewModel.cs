using ShopProject.Controls.MessegeBox.Enum;
using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Setting;
using ShopProject.Services.Integration.Printing;
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Modules.Control.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingPrintingCheckViewModel : ViewModel<SettingPrintingCheckViewModel> , IViewModelLoadResourse
    { 
        private ICommand _saveSettingCommand;
        private ICommand _printTestCheckCommand; 
        private ISettingService _settingService;
        private IPrintingFiscalCheckService _printingFiscalCheckService;
        private readonly IMessageBoxControlService _messageBoxControlService;
        public SettingPrintingCheckViewModel(ISettingService settingService , IPrintingFiscalCheckService printingFiscalCheckService, IMessageBoxControlService messageBoxControlService)
        {
            _printingFiscalCheckService = printingFiscalCheckService;
            _settingService = settingService;
            _messageBoxControlService = messageBoxControlService;
            _saveSettingCommand = CreateCommand(SaveSetting);
            _printTestCheckCommand = CreateCommand(PrintTest);

            _printerFiscalChekSetting = new PrinterFiscalChekSetting();
            _printers = new List<string>(); 
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
                _printers.Add(printer);
            }

            var setting = _settingService.GetSetting<PrinterFiscalChekSetting>();

            if(setting == null)
            {
                setting = new PrinterFiscalChekSetting();
                return;
            }
            PrinterFiscalChekSetting = setting; 

            FiscalCheck fiscalCheck = new FiscalCheck();
            fiscalCheck.CreateFisckalCheck(
                new List<ShopProject.Model.Domain.Product.Product>() 
                {
                    new ShopProject.Model.Domain.Product.Product()
                    {
                        NameProduct ="Товар 1",
                        Price = 100,
                        Count =10 ,  
                        Articule = "1234",
                        Code = "1234567891234", 
                    }
                },
                new ShopProject.Model.Domain.Operation.Operation() { }, 
                new ShopProject.Model.Domain.User.User() { }, 
                new ShopProject.Model.Domain.OperationRecorder.OperationRecorder() { } , new ShopProject.Model.Domain.TaxObject.TaxObject(), PrinterFiscalChekSetting);


            TemplatePrintingCheck = fiscalCheck.GetCheck();
        }
        private List<string> _printers;
        public List<string> Printers
        {
            get { return _printers; }
            set { _printers = value; OnPropertyChanged(nameof(Printers)); }
        }


        private PrinterFiscalChekSetting _printerFiscalChekSetting;
        public PrinterFiscalChekSetting PrinterFiscalChekSetting
        {
            get { return _printerFiscalChekSetting; }
            set { _printerFiscalChekSetting = value; OnPropertyChanged(nameof(PrinterFiscalChekSetting));  }
        }

        private UserControl _templatePrintingCheck { get; set; }
        public UserControl TemplatePrintingCheck 
        {
            get { return _templatePrintingCheck; }
            set { _templatePrintingCheck = value; OnPropertyChanged(nameof(TemplatePrintingCheck)); }
        }


        public ICommand SaveSettingCommand => _saveSettingCommand;

        private void SaveSetting()
        {
            _settingService.SetSetting<PrinterFiscalChekSetting>(PrinterFiscalChekSetting);
            _messageBoxControlService.Show("Налаштування збережено", "Успішно", MessageBoxType.Success, "SettingSnadow");
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
