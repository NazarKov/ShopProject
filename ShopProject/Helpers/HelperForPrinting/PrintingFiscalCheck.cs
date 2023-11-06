using NPOI.SS.Formula.Functions;
using QRCoder;
using ShopProject.DataBase.Model;
using ShopProject.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using ZXing;
using FontFamily = System.Windows.Media.FontFamily;
using Image = System.Windows.Controls.Image;

namespace ShopProject.Helpers.HelperForPrinting
{
    internal class PrintingFiscalCheck
    {
        private string _printer;

        private string _nameShop;
        private string _nameSeller;
        private string _nameFop;
        private string region, district, city, street, house;

        public PrintingFiscalCheck() 
        {
            _nameShop = AppSettingsManager.GetParameterFiles("NameShop").ToString();
            _nameSeller = AppSettingsManager.GetParameterFiles("NameSeller").ToString();
            _nameFop = AppSettingsManager.GetParameterFiles("NameFop").ToString();
            region = AppSettingsManager.GetParameterFiles("Region").ToString();
            district = AppSettingsManager.GetParameterFiles("District").ToString();
            city = AppSettingsManager.GetParameterFiles("Citi").ToString();
            street = AppSettingsManager.GetParameterFiles("Streer").ToString();
            house = AppSettingsManager.GetParameterFiles("House").ToString();

            _printer = AppSettingsManager.GetParameterFiles("PrinterCheck").ToString(); ;
        }

        private Section setTextCheck(string text)
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
        public void PrintCheck(List<Goods> products, string id, Operation operation)
        {
            try
            {
                PrintDialog printDlg = new PrintDialog();
                FlowDocument document = new FlowDocument();

                document.Name = "FlowDoc";


                writeHeaderCheck(document, id);
                writeBodyCheck(products, document, operation);
                writeQrCodeCheck(operation, document, id);
                writeFooterCheck(document, operation);

                IDocumentPaginatorSource idpSource = document;

                if (_printer == null || _printer == string.Empty || _printer == " ")
                {
                    throw new Exception("Ви не вказали принтера");
                }

                printDlg.PrintQueue = new System.Printing.PrintQueue(new System.Printing.PrintServer(), _printer);
                printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void writeHeaderCheck(FlowDocument doc , string id)
        {
            doc.Blocks.Add(setTextCheck("" +
                  $"\n   {_nameFop}        " +
                  $"\n                     {_nameShop}            " +
                  $"\n {region}, {district}, {city}," +
                  $"\n                    {street}, {house}" +
                  $"\n                           ІД " + id + "         " +
                  $"\n                     Касир {_nameSeller}     "));
        }

        private void writeBodyCheck(List<Goods> goods, FlowDocument doc,Operation operation)
        {
            string str = string.Empty;
            foreach (Goods product in goods)
            {
                str += "\nУКТ ЗЕД " + product.codeUKTZED.code;
                str += "\nШтрих-код товару:" + product.code + "\n";
                if (product.name.Length > 50)
                {
                    string[] name = product.name.Split(" ");
                    name[5] += "\n";
                    str += name.ToString();
                }
                else
                {
                    str += product.name + "\n";
                }
                str += product.count + " X " + product.price + "                                               " + (product.count * product.price);
            }
            doc.Blocks.Add(setTextCheck("" +
                    "--------------------------------------------------------------    " + str + "" +
                    "\n--------------------------------------------------------------     " +
                    "\nСУМА                                                       " + operation.totalPayment +
                    "\nБез ПДВ" +
                    "\n--------------------------------------------------------------     " +
                   $"\nГОТІВКОВА                                             {operation.buyersAmount}" +
                   $"\nРешта                                                         {operation.restPayment.ToString("0.00")}"));

        }
        private void writeQrCodeCheck(Operation operation , FlowDocument doc , string id)
        {

            string text = $"https://cabinet.tax.gov.ua/cashregs/check?mac={operation.mac}&date={operation.createdAt.ToString("yyyyMMdd")}&time={operation.createdAt.ToString("HHmm")}&id={id}&sm={operation.totalPayment.ToString("0")}&fn={operation.fiscalNumberRRO}";


            Bitmap btm = GenerateQRCode(text);
            BitmapSource btmsourse = ConvertAndCopyBitmap.BitmapForBitmapSource(btm);
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
        }
        private void writeFooterCheck(FlowDocument doc,Operation operation)
        {

            doc.Blocks.Add(setTextCheck($"" +
                $"\nФН чека:{operation.numberPayment}                        {operation.createdAt.ToString("dd.MM.yyyy")} {operation.createdAt.ToString("HH:mm:ss")}" +
                $"\nФН ПРРО:{operation.fiscalNumberRRO}       Режим роботи:онлайн" +
                $"\n                           ФІКСАЛЬНИЙ ЧЕК"));
            //$"\n                           НАЗВА ПРОГРАМИ"));

        }



        public Bitmap GenerateQRCode(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);


            return qrCodeImage;
        }

       
    }
}
