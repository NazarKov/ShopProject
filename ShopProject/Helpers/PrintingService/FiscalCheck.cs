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
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShopProject.Helpers.PrintingService
{
    internal class FiscalCheck
    { 
        public TemplatePrintingCheck _check; 
        public FiscalCheck()
        {
            _check = new TemplatePrintingCheck();
        }
        public TemplatePrintingCheck GetCheck()
        {
            return _check;
        }

        public void CreateFisckalCheck(List<Product> products, string id, Operation operation, User seller, OperationRecorder operationRecorder , ObjectOwner objectOwner = null)
        { 
            _check = new TemplatePrintingCheck();
            TemplatePrintingCheckBody body;


            if (operationRecorder != null&& operationRecorder.ObjectOwner!=null && objectOwner!=null) 
            {

                _check.NameFop.Text = objectOwner.NameOwner;
                _check.NameShop.Text = objectOwner.NameObject;
                _check.Seller.Text = "Касир " + seller.FullName;

                List<string> address = operationRecorder.ObjectOwner.Address.Split(',').ToList();

                var count = address.Count / 2;

                for (int i = 0; i < count; i++)
                {
                    _check.RegionDistrictCiti.Text += address.ElementAt(i) + " , ";
                }

                for (int i = count; i < address.Count; i++)
                {
                    if (i == address.Count-1)
                    {
                        _check.StreetHouse.Text += address.ElementAt(i);
                    }
                    else
                    {
                        _check.StreetHouse.Text += address.ElementAt(i) + " , ";
                    }
                }
                _check.Id.Text = "ID " + id;
            } 

            double Height = 0;
            for (int i = 0; i < products.Count; i++)
            {
                body = new TemplatePrintingCheckBody();


                Height += 80;
                body.Height = 80;
                body.codeUKTZED.Text = "УКТ ЗЕД: " + products[i].CodeUKTZED.Code.ToString();
                body.codeGoods.Text = "Штрих-код: " + products[i].Code.ToString();
                body.nameGodos.Text = products[i].NameProduct.ToString();
                body.countGoods.Text = ((decimal)products[i].Count).ToString("0.00") + " X " + ((decimal)products[i].Price).ToString("0.00");
                body.priceGoods.Text = ((decimal)products[i].Price).ToString("0.00");

                if (products[i].NameProduct.Length > 45)
                {
                    body.HeightNameGoods.Height = new GridLength(32);
                    body.Height += 16;
                    Height += 16;
                }

                if (products[i].Discount != null && products[i].Discount.TotalDiscount == 0)
                {
                    body.nameSaleGoods.Visibility = Visibility.Hidden;
                    body.saleGoods.Visibility = Visibility.Hidden;
                    Height -= 16;
                    body.Height -= 16;
                }
                else
                {
                    if (products[i].Discount != null)
                    {
                        body.saleGoods.Text = products[i].Discount.TotalDiscount.ToString();
                    }
                }
                _check.templateGoods.Children.Add(body);
            }
            _check.bodyHeight.Height = new GridLength(Height);

            if (operation.Discount != null  && operation.Discount.TotalDiscount != 0)
            {
                _check.totalsale.Text = operation.Discount.TotalDiscount.ToString("0.00");
            }
            else
            {
                _check.totalsalelable.Visibility = Visibility.Collapsed;
                _check.totalsale.Visibility = Visibility.Collapsed;
            }


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
