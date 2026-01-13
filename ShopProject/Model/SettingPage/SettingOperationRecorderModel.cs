using ShopProject.Helpers;
using ShopProject.UIModel.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.SettingPage
{
    internal class SettingOperationRecorderModel
    {

        public SettingOperationRecorderModel() { }

        public OperationRecorderSetting GetSetting()
        {
            try
            {
                var json = AppSettingsManager.GetParameterFiles("OperationRecorder").ToString();
                if (json != null)
                {
                    var setting = OperationRecorderSetting.Deserialize(json);

                    if (setting != null)
                    {
                        return setting;
                    }
                }
                return new OperationRecorderSetting();
            }
            catch 
            {
                return new OperationRecorderSetting();
            }
        }

        public bool SaveSetting(bool isTestMode , string deleteBarCode)
        {
            try
            {
                var json = AppSettingsManager.GetParameterFiles("OperationRecorder").ToString();
                if (json != null) 
                {
                    var setting = OperationRecorderSetting.Deserialize(json);

                    if(setting == null)
                    {
                        setting = new OperationRecorderSetting();
                    } 
                    setting.IsTestMode = isTestMode;
                    setting.DeleteBarCode = deleteBarCode;

                    AppSettingsManager.SetParameterFile("OperationRecorder", setting.Serialize()); 
                } 
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
