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
        private List<Product> _products;

        public OrderCheck()
        {
            _stringCheck = string.Empty;
            _nameShop = string.Empty;
            _nameSeller = string.Empty;
            _title = string.Empty;
            _description = string.Empty;
            _price = string.Empty;
            _products = new List<Product>();
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
            run.FontSize = 10;

            p1.Inlines.Add(run);

            sec.Blocks.Add(p1);
            return sec;
        }

        public void PrintChek(List<Product> products,string id,Order order ,Messe mac,DateTime dateTime)
        {
            try
            {
                // Create a PrintDialog  
                PrintDialog printDlg = new PrintDialog();
                // Create a FlowDocument dynamically.  
                FlowDocument doc = new FlowDocument();

                doc.Name = "FlowDoc";

                string str="";

                foreach (Product product in products)
                {
                    str += "\n     УКТ ЗЕД 9507";
                    str += "\n     Штрих-код товару:" + product.code + "\n     " + product.name + "\n     " + product.count + ",000 X " + product.price + ",00" + "                                               " + (product.count * product.price)+",00";
                }

                // Add Section to FlowDocument  
                doc.Blocks.Add(setTextChek("" +
                    "\n        КОРНІЙЧУК СЕРГІЙ ВОЛОДИМИРОВИЧ        " +
                    "\n                          Магазин Дім Рибалки            " +
                    "\n      Волинська область, Луцький район, м.Рожище," +
                    "\n                         вул.Героїв Упа, 2а, підвал" +
                    "\n                                ІД " + id+"         " +
                    "\n                          Касир КОРНІЙЧУК С. В.     "));

                doc.Blocks.Add(setTextChek("" +
                     "     --------------------------------------------------------------    "+str+"" +
                     "\n     --------------------------------------------------------------     " +
                     "\n     СУМА                                                          " + order.suma+",00" +
                     "\n     Без ПДВ" +
                     "\n     --------------------------------------------------------------     " +
                    $"\n     ГОТІВКОВА                                                {order.userSuma}.00" +
                    $"\n     Решта                                                            {order.rest}.00"));



                string text = $"https://cabinet.tax.gov.ua/cashregs/check?mac={mac.mac}&date={dateTime.ToString("yyyyMMdd")}&time={dateTime.ToString("HHmm")}&id={mac.id}&sm={order.suma}&fn=4000512773";

                string qrtext = text; //считываем текст из TextBox'a

                Bitmap btm = GenerateQRCode(text);
                BitmapSource btmsourse = ConvertBitmapToBitmapSource(btm);
                Image image = new Image
                {
                    Source = btmsourse,
                    Height = 80,
                    Width = 80,
                    
                };


                BlockUIContainer blockUIContainer = new BlockUIContainer(image);
                blockUIContainer.Margin = new Thickness() { Right = 150 };
                doc.Blocks.Add(blockUIContainer);

                //кюар код

                doc.Blocks.Add(setTextChek($"" +
                    $"\n     ФН чека:1234567899              10.07.2023 19:25:45" +
                    $"\n     ФН ПРРО:1234567899       Режим роботи:онлайн" +
                    $"\n                                ФІКСАЛЬНИЙ ЧЕК" +
                    $"\n                                НАЗВА ПРОГРАМИ"));

                //doc.Blocks.Add(sec);

                // Create IDocumentPaginatorSource from FlowDocument  
                IDocumentPaginatorSource idpSource = doc;
                // Call PrintDocument method to send document to printer  
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
        static BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
