using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Common;
using ZXing;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing.Drawing2D;
using System.Windows.Media.Media3D;
using System.Drawing.Imaging;
using System.IO;
using System.Numerics;
using System.Windows.Controls;
using Image = System.Windows.Controls.Image;
using Validation = ShopProject.Helpers.Validation;

namespace ShopProject.Helpers.HelperForPrinting
{
    internal class PrintingSticker
    {
        private string _printer;
        private Bitmap _sticker;
        private int _width;
        private int _height;

        private int sizeText = 12;
        private string nameFont = "Verdana";

        private bool isShowNameCompany { get; set; }
        private bool isShowProductBarCode { get; set; }
        private bool isShowProductName { get; set; }
        private bool isShowProductDescription { get; set; }


        public PrintingSticker ()
        {
            _printer = AppSettingsManager.GetParameterFiles("PrinterSticker").ToString();
            _height = 300;
            _width = 500;
            this.isShowNameCompany = true;
            this.isShowProductBarCode = true;
            this.isShowProductName = true;
            this.isShowProductDescription = true;
        }
        public void SetShowTextInImage(bool isShowNameCompany, bool isShowProductBarCode, bool isShowProductName, bool isShowProductDescription)
        {
            this.isShowNameCompany = isShowNameCompany;
            this.isShowProductBarCode = isShowProductBarCode;
            this.isShowProductName = isShowProductName;
            this.isShowProductDescription = isShowProductDescription;
        }

        public BitmapImage CreateSticker(string company, string name, string description, string code, bool isValidation)
        {
            try
            {
                if (isValidation)
                {
                    ValidIsNull(company, name, code, description);
                    CheckLength(company, code, name, description);
                }


                _sticker = new Bitmap(_width, _height);
                _sticker = ConvertAndCopyBitmap.CopyBitmapInBitmap(CreateBarCodeInBitMap(code), 0, 160, _width, _height, 50);

                CreateBorderSticker(_sticker);
                SetTextSticker(name, company, description, code, _sticker);

                   
                AppSettingsManager.SetParameterFile("LastBarCode", code);

                return ConvertAndCopyBitmap.BitmapForBitmapImage(_sticker);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return null;
            }
        }

        private Bitmap CreateBarCodeInBitMap(string barCode)
        {
            Bitmap bitmapCode = new Bitmap(_width,_height);

            BarcodeWriterPixelData writer = new BarcodeWriterPixelData()
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = _height,
                    Width = _width,
                },

            };

            var pixelData = writer.Write(barCode);
            var bitmapData = bitmapCode.LockBits(new Rectangle(0, 0, _width, _height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            bitmapCode.UnlockBits(bitmapData);
           return bitmapCode;
        }
        
        private void SetTextSticker(string name, string company, string description, string code, Bitmap bitmap)
        {
            if (isShowNameCompany)
                CreateTextSticker(company, bitmap, GetCenterString(company), 30, new Font(nameFont, sizeText));
            if (isShowProductBarCode)
                CreateTextSticker(code, bitmap, GetCenterString(code), 260, new Font(nameFont, sizeText));
            if (isShowProductName)
                CreateTextSticker("Назва товару : " + name, bitmap, 30, 70, new Font(nameFont, sizeText));
            if (isShowProductDescription)
                CreateTextSticker("Опис товару : " + description, bitmap, 30, 100, new Font(nameFont, sizeText));
        }

        private int GetCenterString(string text)
        {
            return (480 / 2) - ((text.Length * 6) / 2);
        }
        
        private void CreateTextSticker(string text, Bitmap bitmap, int x, int y, Font font)
        {
            RectangleF rectf = new RectangleF(x, y, sizeText * text.Length, 60);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.DrawString(text, font, Brushes.Black,rectf);
            graphics.Flush();
        }


        private void CreateBorderSticker(Bitmap bitmap)
        {
            for (int i = 20; i < 480; i++)
            {
                bitmap.SetPixel(i, 20, Color.Black);//верх
                bitmap.SetPixel(i, 280, Color.Black);//низ
                bitmap.SetPixel(i, 60, Color.Black);//вище середини
                bitmap.SetPixel(i, 150, Color.Black);//ниже середини
            }
            for (int i = 20; i < 280; i++)
            {
                bitmap.SetPixel(20, i, Color.Black);//право
                bitmap.SetPixel(480, i, Color.Black);//ліво
            }
        }

        private void CheckLength(string company, string code, string name, string description)
        {
            if (isShowNameCompany)
            {
                if (company.Length <= 3)
                {
                    throw new Exception("мінімальна довжина назви компанії 3 символа");
                }
            }
            if (isShowProductBarCode)
            {
                if (code.Length <= 3)
                {
                    throw new Exception("мінімальна довжина цифер штрихкоду 3 символа");
                }
                else if (code.Length > 15)
                {
                    throw new Exception("максимальна довжина цифер штрихкоду 15 символа");
                }
            }
            if (isShowProductName)
            {
                if (name.Length > 30)
                {
                    throw new Exception("максимальна довжина назви продукта 30 символа");
                }
            }
            if (isShowProductDescription)
            {
                if (description.Length > 30)
                {
                    throw new Exception("максимальна довжина опису продукта 30 символа");
                }
            }
        }

        private void ValidIsNull(string company, string name, string code, string description)
        {
            if (isShowProductBarCode)
                Validation.ItemChekIsNull(code, typeof(string), "штрихкод");
            if (isShowNameCompany)
                Validation.ItemChekIsNull(company, typeof(string), "компанія");
            if (isShowProductName)
                Validation.ItemChekIsNull(name, typeof(string), "назва");
            if (isShowProductDescription)
                Validation.ItemChekIsNull(description, typeof(string), "опис");
        }

        public BitmapImage Clear()
        {
            _sticker = new Bitmap(1, 1);
            return ConvertAndCopyBitmap.BitmapForBitmapImage(_sticker);
        }

        public bool PrintSticker()
        {
            try
            {
                if (_printer == null || _printer == string.Empty || _printer == " ")
                {
                    throw new Exception("Ви не вказали принтера");
                }
                Image image = new Image();
                image.Source = ConvertAndCopyBitmap.BitmapForBitmapImage(_sticker);
                PrintDialog printDialog = new PrintDialog();
                printDialog.PrintQueue = new System.Printing.PrintQueue(new System.Printing.PrintServer(), _printer);
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(image, "file");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
       

      
    }
}
