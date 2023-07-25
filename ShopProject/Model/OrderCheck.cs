using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

using FontFamily = System.Windows.Media.FontFamily;
using System.Windows.Media.Imaging;
using QRCoder;
using QRCode = QRCoder.QRCode;
using Image = System.Windows.Controls.Image;
using ShopProject.Helpers.DFSAPI;
using ZXing.QrCode.Internal;
using System.Windows.Media.Media3D;
using ZXing;
using System.IO;
using ZXing.QrCode;
using ZXing.Common;
using Color = System.Drawing.Color;

namespace ShopProject.Model
{
    internal class OrderCheck
    {
        private string _numberChek;
        private string _stringCheck;
        private string _nameShop;
        private string _nameSeller;
        private string _title;
        private string _description;
        private string _price;
        private List<Goods> _products;

        public OrderCheck()
        {
            _stringCheck = string.Empty;
            _nameShop = string.Empty;
            _nameSeller = string.Empty;
            _title = string.Empty;
            _description = string.Empty;
            _price = string.Empty;
            _products = new List<Goods>();
        }

        public void drawingChek()
        {
            try
            {
                FlowDocument document = new FlowDocument();
                


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Section setTextChek(string text)
        {
            Section sec = new Section();
            Paragraph p1 = new Paragraph();

            Run run = new Run(text);
            run.FontFamily = new FontFamily("Times New Roman");
            run.FontSize = 9;

            p1.Inlines.Add(run);

            sec.Blocks.Add(p1);
            return sec;
        }

        public void PrintChek(List<Goods> products,string id,Operation order ,Messe mac,DateTime dateTime)
        {
            try
            {
                // Create a PrintDialog  
                PrintDialog printDlg = new PrintDialog();
                // Create a FlowDocument dynamically.  
                FlowDocument doc = new FlowDocument();

                doc.Name = "FlowDoc";

                string str="";

                foreach (Goods product in products)
                {
                    str += "\nУКТ ЗЕД 9507";
                    str += "\nШтрих-код товару:" + product.code + "\n";
                    if(product.name.Length >50)
                    {
                        string[] name = product.name.Split(" ");
                        name[5] += "\n";
                        str += name.ToString();
                    }
                    else
                    {
                        str += product.name + "\n";
                    }
                    str+=product.count + ",00 X " + product.price + ",00" + "                                               " + (product.count * product.price)+",00";
                }

                // Add Section to FlowDocument  
                doc.Blocks.Add(setTextChek("" +
                    "\n   КОРНІЙЧУК СЕРГІЙ ВОЛОДИМИРОВИЧ        " +
                    "\n                     Магазин Дім Рибалки            " +
                    "\n Волинська область, Луцький район, м.Рожище," +
                    "\n                    вул.Героїв Упа, 2а, підвал" +
                    "\n                           ІД " + mac.id+"         " +
                    "\n                     Касир КОРНІЙЧУК С. В.     "));

                doc.Blocks.Add(setTextChek("" +
                     "--------------------------------------------------------------    "+str+"" +
                     "\n--------------------------------------------------------------     " +
                     "\nСУМА                                                          " + order.suma+",00" +
                     "\nБез ПДВ" +
                     "\n--------------------------------------------------------------     " +
                    $"\nГОТІВКОВА                                                {order.userSuma}.00" +
                    $"\nРешта                                                            {order.rest}.00"));



                string text = $"https://cabinet.tax.gov.ua/cashregs/check?mac={mac.mac}&date={dateTime.ToString("yyyyMMdd")}&time={dateTime.ToString("HHmm")}&id={mac.id}&sm={order.suma}&fn=4000512773";

                string qrtext = text; //считываем текст из TextBox'a

                

                Bitmap btm = GenerateQRCode(text);
                BitmapSource btmsourse = ConvertBitmapToBitmapSource(btm);
                Image image = new Image
                {
                    Source = btmsourse,
                    Height = 120,
                    Width = 120,
                    
                };


                BlockUIContainer blockUIContainer = new BlockUIContainer(image);
                //blockUIContainer.Padding = new Thickness() { Left=50 , Right=50 , Bottom = 50 , Top = 50};
                blockUIContainer.Margin = new Thickness() { Right = 200 };
                doc.Blocks.Add(blockUIContainer);

                //кюар код

                doc.Blocks.Add(setTextChek($"" +
                    $"\nФН чека:{order.LocalNumber}                        {dateTime.ToString("dd.MM.yyyy")} {dateTime.ToString("HH:mm:ss")}" +
                    $"\nФН ПРРО:4000512773       Режим роботи:онлайн" +
                    $"\n                           ФІКСАЛЬНИЙ ЧЕК" +
                    $"\n                           НАЗВА ПРОГРАМИ"));

                //doc.Blocks.Add(sec);

                // Create IDocumentPaginatorSource from FlowDocument  
                IDocumentPaginatorSource idpSource = doc;
                // Call PrintDocument method to send document to printer  
                printDlg.PrintQueue = new System.Printing.PrintQueue(new System.Printing.PrintServer(), "58mm Series Printer");
                printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Bitmap GenerateQRCode(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            
            return qrCodeImage;
        }
        public Bitmap qRcODE(string inputText,int width , int height)
        {
            QRCodeWriter qRCodeWriter = new QRCodeWriter();
            BitMatrix matrix= qRCodeWriter.encode(inputText,BarcodeFormat.QR_CODE,width,height);


            var qrImage = ConvertBitMatrixToBitmap(matrix);
            return qrImage;
        }
        static BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
        public Bitmap ConvertBitMatrixToBitmap(BitMatrix bitMatrix)
        {
            int width = bitMatrix.Width;
            int height = bitMatrix.Height;
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bitmap.SetPixel(x, y, bitMatrix[x, y] ? Color.Black : Color.White);
                }
            }

            return bitmap;
        }
    }
}
