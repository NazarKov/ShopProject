 
using QRCoder;
using ShopProject.Resource.template;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZXing;
using FontFamily = System.Windows.Media.FontFamily;
using Image = System.Windows.Controls.Image;

namespace ShopProject.Helpers.PrintingServise
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
            //_nameShop = AppSettingsManager.GetParameterFiles("NameShop").ToString();
            //_nameSeller = AppSettingsManager.GetParameterFiles("NameSeller").ToString();
            //_nameFop = AppSettingsManager.GetParameterFiles("NameFop").ToString();
            //region = AppSettingsManager.GetParameterFiles("Region").ToString();
            //district = AppSettingsManager.GetParameterFiles("District").ToString();
            //city = AppSettingsManager.GetParameterFiles("Citi").ToString();
            //street = AppSettingsManager.GetParameterFiles("Streer").ToString();
            //house = AppSettingsManager.GetParameterFiles("House").ToString();

            //_printer = AppSettingsManager.GetParameterFiles("PrinterCheck").ToString(); ;
        }
        //public void PrintCheck(List<ProductEntiti> goods, string id, OperationEntiti operation)
        //{
        //    try
        //    {
        //        PrintDialog printDlg = new PrintDialog();

        //        TemplatePrintingCheck template = new TemplatePrintingCheck();
        //        TemplatePrintingCheckBody templateBody;
        //        template.NameFop.Text = _nameFop;
        //        template.NameShop.Text = _nameShop;
        //        template.Seller.Text = "Касир "+_nameSeller;
        //        template.RegionDistrictCiti.Text = region + ", " + district + ", " + city;
        //        template.StreetHouse.Text = street + ", " + house;
        //        template.Id.Text = "ID " + id;

        //        double Height = 0;
        //        for (int i = 0; i< goods.Count; i++)
        //        {
        //            templateBody = new TemplatePrintingCheckBody();
                    

        //            Height += 80;
        //            templateBody.Height = 80;
        //            templateBody.codeUKTZED.Text = "УКТ ЗЕД: " + goods[i].CodeUKTZED.Code.ToString();
        //            templateBody.codeGoods.Text = "Штрих-код: " + goods[i].Code.ToString();
        //            templateBody.nameGodos.Text = goods[i].NameProduct.ToString();
        //            templateBody.countGoods.Text = ((decimal)goods[i].Count).ToString("0.00") + " X " + ((decimal)goods[i].Price).ToString("0.00");
        //            templateBody.priceGoods.Text = ((decimal)goods[i].Price).ToString("0.00");

        //            if (goods[i].NameProduct.Length > 45)
        //            {
        //                templateBody.HeightNameGoods.Height = new GridLength(32);
        //                templateBody.Height += 16;
        //                Height += 16;
        //            }

        //            //if (goods[i].Sales == 0 || goods[i].Sales == null)
        //            //{
        //            //    templateBody.nameSaleGoods.Visibility = Visibility.Hidden;
        //            //    templateBody.saleGoods.Visibility = Visibility.Hidden;
        //            //    Height -= 16;
        //            //    templateBody.Height -= 16;
        //            //}
        //            //else
        //            //{
        //            //    templateBody.saleGoods.Text = goods[i].Sales.ToString();
        //            //}
        //            template.templateGoods.Children.Add(templateBody);
        //        }
        //        template.bodyHeight.Height = new GridLength(Height);

        //        if (operation.FormOfPayment == 0)
        //        {
        //            template.buyersAmountCash.Text = operation.BuyersAmount.ToString("0.00");
        //            template.nameBuyersAmountCard.Visibility = Visibility.Hidden;
        //            template.buyersAmountCard.Visibility = Visibility.Hidden;
        //        }
        //        else
        //        {
        //            template.buyersAmountCard.Text = operation.BuyersAmount.ToString("0.00");
        //            template.nameBuyersAmountCash.Visibility = Visibility.Hidden;
        //            template.buyersAmountCash.Visibility = Visibility.Hidden;
        //        }


        //        template.totalSum.Text = operation.TotalPayment.ToString("0.00");
        //        template.totalRest.Text = operation.RestPayment.ToString("0.00");

        //        template.FNCheck.Text = "ФН чека: " + operation.NumberPayment;
        //        template.FNRRo.Text = "ФН ПРРО: " + operation.FiscalNumberRRO;

        //        template.QRCOde.Source = writeQrCodeCheck(operation,id);

        //        template.date.Text = DateTime.Now.ToString();

        //        template.grid.Margin = new Thickness(0);

        //        if (_printer == null || _printer == string.Empty || _printer == " ")
        //        {
        //            throw new Exception("Ви не вказали принтера");
        //        }

        //        printDlg.PrintQueue = new System.Printing.PrintQueue(new System.Printing.PrintServer(), _printer);
        //        printDlg.PrintVisual(template.grid, "Check");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private ImageSource writeQrCodeCheck(OperationEntiti operation , string id)
        //{

        //    string text = $"https://cabinet.tax.gov.ua/cashregs/check?mac={operation.MAC}&date={operation.CreatedAt.ToString("yyyyMMdd")}&time={operation.CreatedAt.ToString("HHmm")}&id={id}&sm={operation.TotalPayment.ToString("0")}&fn={operation.FiscalNumberRRO}";
        //    Bitmap btm = GenerateQRCode(text);

        //    ImageSource imageSource = Imaging.CreateBitmapSourceFromHBitmap(
        //        btm.GetHbitmap(),
        //        IntPtr.Zero,
        //        Int32Rect.Empty,
        //        BitmapSizeOptions.FromEmptyOptions());
        //    return imageSource;
        //}

        //public Bitmap GenerateQRCode(string data)
        //{
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
        //    QRCode qrCode = new QRCode(qrCodeData);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(20);


        //    return qrCodeImage;
        //}

       
    }
}
