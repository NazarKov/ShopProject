using QRCoder;
using ShopProject.Resource.template;
using ShopProject.UIModel.ObjectOwnerPage;
using ShopProject.UIModel.OperationRecorderPage;
using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.StoragePage;
using ShopProject.UIModel.UserPage;
using System;
using System.Collections.Generic;
using System.Drawing; 
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShopProject.Helpers.PrintingService
{
    internal class FiscalCheck
    { 
        private TemplatePrintingCheck _check; 
        public FiscalCheck()
        {
            _check = new TemplatePrintingCheck();
        }
        public TemplatePrintingCheck GetCheck()
        {
            return _check;
        }

        public void CreateFisckalCheck(List<Product> goods, string id, Operation operation, User seller, ObjectOwner objectOwner, User Fop, OperationRecorder operationRecorder)
        { 
            _check = new TemplatePrintingCheck();
            TemplatePrintingCheckBody body;

            _check.NameFop.Text = Fop.FullName;
            _check.NameShop.Text = objectOwner.NameObject;
            //template.Seller.Text = "Касир " + seller.FullName;
            //template.RegionDistrictCiti.Text = region + ", " + district + ", " + city;
            //template.StreetHouse.Text = street + ", " + house;
            _check.Id.Text = "ID " + id;


            double Height = 0;
            for (int i = 0; i < goods.Count; i++)
            {
                body = new TemplatePrintingCheckBody();


                Height += 80;
                body.Height = 80;
                body.codeUKTZED.Text = "УКТ ЗЕД: " + goods[i].CodeUKTZED.Code.ToString();
                body.codeGoods.Text = "Штрих-код: " + goods[i].Code.ToString();
                body.nameGodos.Text = goods[i].NameProduct.ToString();
                body.countGoods.Text = ((decimal)goods[i].Count).ToString("0.00") + " X " + ((decimal)goods[i].Price).ToString("0.00");
                body.priceGoods.Text = ((decimal)goods[i].Price).ToString("0.00");

                if (goods[i].NameProduct.Length > 45)
                {
                    body.HeightNameGoods.Height = new GridLength(32);
                    body.Height += 16;
                    Height += 16;
                }

                //if (goods[i]. == 0 || goods[i].Sales == null)
                //{
                //    templateBody.nameSaleGoods.Visibility = Visibility.Hidden;
                //    templateBody.saleGoods.Visibility = Visibility.Hidden;
                //    Height -= 16;
                //    templateBody.Height -= 16;
                //}
                //else
                //{
                //    templateBody.saleGoods.Text = goods[i].Sales.ToString();
                //}
                _check.templateGoods.Children.Add(body);
            }
            _check.bodyHeight.Height = new GridLength(Height);

            if (operation.TypePayment == 0)
            {
                _check.buyersAmountCash.Text = operation.BuyersAmount.ToString("0.00");
                _check.nameBuyersAmountCard.Visibility = Visibility.Hidden;
                _check.buyersAmountCard.Visibility = Visibility.Hidden;
            }
            else
            {
                _check.buyersAmountCard.Text = operation.BuyersAmount.ToString("0.00");
                _check.nameBuyersAmountCash.Visibility = Visibility.Hidden;
                _check.buyersAmountCash.Visibility = Visibility.Hidden;
            }


            _check.totalSum.Text = operation.TotalPayment.ToString("0.00");
            _check.totalRest.Text = operation.RestPayment.ToString("0.00");

            _check.FNCheck.Text = "ФН чека: " + operation.NumberPayment;
            _check.FNRRo.Text = "ФН ПРРО: " + operationRecorder.FiscalNumber;

            _check.QRCOde.Source = WriteQrCodeCheck(operation, id, operationRecorder.FiscalNumber);

            _check.date.Text = DateTime.Now.ToString();

            _check.grid.Margin = new Thickness(0); 
        }
        private ImageSource WriteQrCodeCheck(Operation operation, string id, string FiscalNumberRRO)
        {

            string text = $"https://cabinet.tax.gov.ua/cashregs/check?mac={operation.MAC}" +
                $"&date={operation.CreatedAt.ToString("yyyyMMdd")}" +
                $"&time={operation.CreatedAt.ToString("HHmm")}" +
                $"&id={id}&sm={operation.TotalPayment.ToString("0")}&fn={FiscalNumberRRO}";
            Bitmap btm = GenerateQRCode(text);

            ImageSource imageSource = Imaging.CreateBitmapSourceFromHBitmap(
                btm.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            return imageSource;
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
