using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.SettingPage
{
    internal class SettingPrintingCheckModel
    {
        public SettingPrintingCheckModel() { }

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
            };
        }
    }
}
