using System.Drawing;
using ZXing;
using ZXing.Common;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows;
using System;
using ShopProject.Helpers.HelperForPrinting;

namespace ShopProject.Model.ToolsPage
{
    internal class CreateStickerModel
    {
        private PrintingSticker _printingSticker;

        public CreateStickerModel()
        {
            _printingSticker = new PrintingSticker();
        }

        public void SetShowTextInImage(bool isShowNameCompany, bool isShowProductBarCode, bool isShowProductName, bool isShowProductDescription)
        {
            _printingSticker.SetShowTextInImage(isShowNameCompany, isShowProductBarCode, isShowProductName, isShowProductDescription);
        }

    
        public BitmapImage CreateBarCode(string company , string name,string description, string code)
        {
            return _printingSticker.CreateSticker(company, name, description, code, true);
        }
        public BitmapImage Clear()
        {
            return _printingSticker.Clear();
        }
        public void Print()
        {
            _printingSticker.PrintSticker();
        }
   
    }
}
