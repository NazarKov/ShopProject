using GreetClient;
using ShopProject.Resource.template;
using ShopProject.UIModel.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShopProject.Helpers.PrintingService
{
    internal class PrintingFiscalCheckServise
    {
        private PrinterFiscalChekSetting _printerSetting;

        public PrintingFiscalCheckServise()
        {
            _printerSetting = new PrinterFiscalChekSetting();
        }

        public void SetSetting(PrinterFiscalChekSetting setting)
        {
            _printerSetting = setting;
        }
        public void PrintCheck(TemplatePrintingCheck check)
        {
            check.grid.Width = _printerSetting.Width;
            check.grid.Measure(new System.Windows.Size(_printerSetting.Width, double.PositiveInfinity));
            check.grid.Arrange(new Rect(0, 0, _printerSetting.Width, check.DesiredSize.Height));
            check.grid.LayoutTransform = new ScaleTransform(_printerSetting.Slcale, _printerSetting.Slcale);
            check.QRCOde.Width = _printerSetting.SizeQrCode;
            check.QRCOde.Height = _printerSetting.SizeQrCode;
            

            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintQueue = new System.Printing.PrintQueue(new System.Printing.PrintServer(), _printerSetting.Printer);
            printDlg.PrintVisual(check.grid, "Check");
        }
    }
}
