using GreetClient;
using ShopProject.Model.Domain.Setting;
using ShopProject.Resource.template;
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Modules.Setting.Interface; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShopProject.Services.Integration.Printing
{
    internal class PrintingFiscalCheckServise :IPrintingFiscalCheckService
    {
        private ISettingService _settingService;

        public PrintingFiscalCheckServise(ISettingService settingService)
        {
            _settingService = settingService;
        } 
        public void PrintCheck(TemplatePrintingCheck check)
        {
            var setting = _settingService.GetSetting<PrinterFiscalChekSetting>();

            check.grid.Width = setting.Width;
            check.grid.Measure(new System.Windows.Size(setting.Width, double.PositiveInfinity));
            check.grid.Arrange(new Rect(0, 0, setting.Width, check.DesiredSize.Height));
            check.grid.LayoutTransform = new ScaleTransform(setting.Slcale, setting.Slcale);
            check.QRCOde.Width = setting.SizeQrCode;
            check.QRCOde.Height = setting.SizeQrCode;
            

            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintQueue = new System.Printing.PrintQueue(new System.Printing.PrintServer(), setting.Printer);
            printDlg.PrintVisual(check.grid, "Check");
        }
    }
}
