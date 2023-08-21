using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.SettingPage
{
    internal class SettingDeviceSettlementOperationsModel
    {
        public SettingDeviceSettlementOperationsModel()
        {

        }

        public void ChekConnection()
        {
            
        }

        public void SaveFieldSetting(string key,string value)
        {
            AppSettingsManager.SetParameterFile(key, value);
        }
        public string GetSettingSetting(string key)
        {
            var setting = AppSettingsManager.GetParameterFiles(key).ToString();
            if(setting == string.Empty||setting==null)
            {
                return string.Empty;
            }
            else
            {
                return setting;
            }

        }
        
    }
}
