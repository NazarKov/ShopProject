using System.Drawing;
using ZXing;
using ZXing.Common;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows;
using System;

namespace ShopProject.Model.ToolsPage
{
    internal class CreateStikerModel
    {
        private RectangleF? rectf;
        private Graphics? g;
        private Bitmap? barCode;
        private int sizeText = 12;
        private string nameFont = "Verdana";

        public CreateStikerModel(){}

        private Bitmap CopyBitmapInBitmap(Bitmap image, int x, int y, int width, int height, int bottom)
        {
            Bitmap resultCopy = new Bitmap(width, height);
            for (int i = x; i < width; i++)
            {
                for(int j = y  ; j<height - bottom; j++)
                {
                    if(i < image.Width & j < image.Height)
                        resultCopy.SetPixel(i, j, image.GetPixel(i,j));
                }
            }

            return resultCopy;
        }

        public BitmapImage CreateBarCode(string company , string name,string deckriptions,string code)
        {
            try
            {
                ValidIsNull(company, name, code, deckriptions);

                barCode = new Bitmap(500, 300);
                barCode = CopyBitmapInBitmap(CreateBarCode(code), 0, 160, 500, 300, 50);

                CreateBorderStiker(barCode);
                SetTextStiker(name, company, deckriptions, code, barCode);

                return Bitmap2BitmapImage(barCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
              
                return null;
            }
        }

        public BitmapImage Clear()
        {
            barCode = new Bitmap(1,1);
            return Bitmap2BitmapImage(barCode);
        }

        private void ValidIsNull(string company,string name,string code,string deckriptions)
        {
            Validation.ItemChekIsNull(code, typeof(string), "штрихкод");
            Validation.ItemChekIsNull(company, typeof(string), "компанія");
            Validation.ItemChekIsNull(name, typeof(string), "назва");
            Validation.ItemChekIsNull(deckriptions, typeof(string), "опис");
        }

        private Bitmap CreateBarCode(string code)
        {
            Bitmap bitmapCode = new Bitmap(500, 300);

            BarcodeWriterPixelData writer = new BarcodeWriterPixelData()
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 300,
                    Width = 500,
                },

            };

            var pixelData = writer.Write(code);
            var bitmapData = bitmapCode.LockBits(new Rectangle(0, 0, 500, 300), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            bitmapCode.UnlockBits(bitmapData);

            return bitmapCode;
        }

        private void CreateBorderStiker(Bitmap bitmap)
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

        private void SetTextStiker(string name,string company,string deckriptions,string code,Bitmap bitmap)
        {
            CreateTextStiker(company, bitmap,GetCenterString(company), 30, new Font(nameFont, sizeText));
            CreateTextStiker("Назва товару : " + name, bitmap, 30, 70, new Font(nameFont, sizeText));
            CreateTextStiker("Опис товару : " + deckriptions, bitmap, 30, 100, new Font(nameFont, sizeText));
            CreateTextStiker(code, bitmap, GetCenterString(code), 260, new Font(nameFont, sizeText));
        }

        private int GetCenterString(string text)
        {
            return (480 / 2) - ((text.Length * 6) / 2);
        }

        private void CreateTextStiker(string text ,Bitmap bitmap ,int x ,int y ,Font font)
        {
            rectf = new RectangleF(x, y, 12 * text.Length, 60);
            g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString(text, font, Brushes.Black, (RectangleF)rectf);
            g.Flush();
        }

        private BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
