using ShopProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.SettingPage
{
    internal class SettingPrintingCheckModel
    {
        //private PrintingFiscalCheck _ordererCheck;
        public SettingPrintingCheckModel() 
        {
            //_ordererCheck = new PrintingFiscalCheck();
        }

        public void Save(params string[] item)
        {
            AppSettingsManager.SetParameterFile("NameShop", item[0]);
            AppSettingsManager.SetParameterFile("NameSeller", item[1]);
            AppSettingsManager.SetParameterFile("NameFop", item[2]);
            AppSettingsManager.SetParameterFile("Region", item[3]);
            AppSettingsManager.SetParameterFile("District", item[4]);
            AppSettingsManager.SetParameterFile("Citi", item[5]);
            AppSettingsManager.SetParameterFile("Streer", item[6]);
            AppSettingsManager.SetParameterFile("House", item[7]);
            AppSettingsManager.SetParameterFile("PrinterCheck", item[8]);
        }
        public List<string> Get()
        {
            return new List<string>() 
            {
                AppSettingsManager.GetParameterFiles("NameShop").ToString(),
                AppSettingsManager.GetParameterFiles("NameSeller").ToString(),
                AppSettingsManager.GetParameterFiles("NameFop").ToString(),
                AppSettingsManager.GetParameterFiles("Region").ToString(),
                AppSettingsManager.GetParameterFiles("District").ToString(),
                AppSettingsManager.GetParameterFiles("Citi").ToString(),
                AppSettingsManager.GetParameterFiles("Streer").ToString(),
                AppSettingsManager.GetParameterFiles("House").ToString(),
                AppSettingsManager.GetParameterFiles("PrinterCheck").ToString(),
            };
        }
        public void PrintTest()
        {
            //_ordererCheck.PrintCheck(new List<DataBase.Model.GoodsEntiti>() {
            //    new DataBase.Model.GoodsEntiti()
            //    {
            //        Code = "123456789101",
            //        NameGoods = "Товар №1",
            //        CodeUKTZED = new DataBase.Model.CodeUKTZEDEntiti(){ Code = "8855"},
            //        Price = 100,
            //        Count = 5,
            //    },
            //    new DataBase.Model.GoodsEntiti()
            //    {
            //        Code = "987654321101",
            //        NameGoods = "Товар №2",
            //        CodeUKTZED = new DataBase.Model.CodeUKTZEDEntiti(){ Code = "8865"},
            //        Price = 200,
            //        Count = 5,
            //    }
            //},"123", new DataBase.Model.OperationEntiti());
        }
    }
}
