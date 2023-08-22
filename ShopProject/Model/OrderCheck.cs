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
        private string _nameShop;
        private string _nameSeller;
        private string _nameFop;
        private string region ,district, city , street ,house;

        public OrderCheck()
        {
            _nameShop = AppSettingsManager.GetParameterFiles("NameShop").ToString();
            _nameSeller = AppSettingsManager.GetParameterFiles("NameSeller").ToString();
            _nameFop = AppSettingsManager.GetParameterFiles("NameFop").ToString();
            region = AppSettingsManager.GetParameterFiles("Region").ToString();
            district = AppSettingsManager.GetParameterFiles("District").ToString();
            city = AppSettingsManager.GetParameterFiles("Citi").ToString();
            street = AppSettingsManager.GetParameterFiles("Streer").ToString();
            house = AppSettingsManager.GetParameterFiles("House").ToString();

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

        public void PrintChek(List<Goods> products,string id,Operation order)
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
                    str += "\nУКТ ЗЕД "+product.codeUKTZED.code;
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
                    str+=product.count +" X " + product.price +"                                               " + (product.count * product.price);
                }

                // Add Section to FlowDocument  
                doc.Blocks.Add(setTextChek("" +
                    $"\n   {_nameFop}        " +
                    $"\n                     {_nameShop}            " +
                    $"\n {region}, {district}, {city}," +
                    $"\n                    {street}, {house}" +
                    $"\n                           ІД " + id+"         " +
                    $"\n                     Касир {_nameSeller}     "));

                doc.Blocks.Add(setTextChek("" +
                     "--------------------------------------------------------------    "+str+"" +
                     "\n--------------------------------------------------------------     " +
                     "\nСУМА                                                       " + order.totalPayment +
                     "\nБез ПДВ" +
                     "\n--------------------------------------------------------------     " +
                    $"\nГОТІВКОВА                                             {order.buyersAmount}" +
                    $"\nРешта                                                         {order.restPayment.ToString("0.00")}"));



                string text = $"https://cabinet.tax.gov.ua/cashregs/check?mac={order.mac}&date={order.createdAt.ToString("yyyyMMdd")}&time={order.createdAt.ToString("HHmm")}&id={id}&sm={order.totalPayment.ToString("0")}&fn={order.fiscalNumberRRO}";

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
                    $"\nФН чека:{order.numberPayment}                        {order.createdAt.ToString("dd.MM.yyyy")} {order.createdAt.ToString("HH:mm:ss")}" +
                    $"\nФН ПРРО:{order.fiscalNumberRRO}       Режим роботи:онлайн" +
                    $"\n                           ФІКСАЛЬНИЙ ЧЕК"));
                    //$"\n                           НАЗВА ПРОГРАМИ"));

                //doc.Blocks.Add(sec);

                // Create IDocumentPaginatorSource from FlowDocument  
                IDocumentPaginatorSource idpSource = doc;
                // Call PrintDocument method to send document to printer  
                //printDlg.PrintQueue = new System.Printing.PrintQueue(new System.Printing.PrintServer(), "58mm Series Printer");
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
