using GreetClient;
using ShopProject.Helpers;
using ShopProject.Helpers.PrintingService;
using ShopProject.Helpers.PrintingServise;
using ShopProject.UIModel.ObjectOwnerPage;
using ShopProject.UIModel.OperationRecorderPage;
using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.SettingPage;
using ShopProject.UIModel.StoragePage;
using ShopProject.UIModel.UserPage;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.SettingPage
{
    internal class SettingPrintingCheckModel
    {
        private FiscalCheck _check;
        private PrintingFiscalCheckServise _servise;
        public SettingPrintingCheckModel()
        {
            _check = new FiscalCheck();
            _servise = new PrintingFiscalCheckServise(); 
        } 
        public void PrintTest()
        {
            try
            {
                var json = AppSettingsManager.GetParameterFiles("PrinterCheck").ToString();
                if(json == null)
                {
                    throw new Exception();
                }

                var setting = PrinterFiscalChekSetting.Deserialize(json);
                if (setting != null)
                {
                    _check.CreateFisckalCheck(new List<Product>() {
                new Product()
                {
                    Code = "123456789101",
                    NameProduct = "Товар №1",
                    CodeUKTZED = new  ProductCodeUKTZED(){ Code = "8855"},
                    Price = 100,
                    Count = 5,
                },
                new Product()
                {
                    Code = "987654321101",
                    NameProduct = "Товар №2",
                    CodeUKTZED = new ProductCodeUKTZED(){ Code = "8865"},
                    Price = 200,
                    Count = 5,
                }
                }, new Operation(), new User(),  new OperationRecorder());

                    _servise.SetSetting(setting);
                    _servise.PrintCheck(_check.GetCheck());
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
