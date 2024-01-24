using ShopProject.Helpers;
using ShopProject.Helpers.HelperForPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.SettingPage
{
    internal class SettingStickerModel
    {
        private PrintingSticker _printingSticker;

        public SettingStickerModel()
        {
            _printingSticker = new PrintingSticker();
        }

        public string Get()
        {
            return AppSettingsManager.GetParameterFiles("PrinterSticker").ToString();
        }
        public void Save(string namePrinter)
        {
            AppSettingsManager.SetParameterFile("PrinterSticker", namePrinter);
        }
        public void PrintTest()
        {
            _printingSticker.CreateSticker("Ваша компанія","Товар №1","Опис до товару №1","123456789101",false);
            _printingSticker.PrintSticker();
        }
    }
}
