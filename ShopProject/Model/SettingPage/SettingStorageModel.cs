using ShopProject.Helpers;
using ShopProject.UIModel.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.SettingPage
{
    internal class SettingStorageModel
    {
        public StorageSetting GetSetting()
        {
            try
            {
                var json = AppSettingsManager.GetParameterFiles("StorageSetting").ToString();
                if (json != null)
                {
                    var setting = StorageSetting.Deserialize(json);

                    if (setting != null)
                    {
                        return setting;
                    }
                }
                return new StorageSetting();
            }
            catch
            {
                return new StorageSetting();
            }
        }

        public bool SaveSetting(int productBarCodeLength , int sertificateBarCodeLength)
        {
            try
            {
                var json = AppSettingsManager.GetParameterFiles("StorageSetting").ToString();
                if (json != null)
                {
                    var setting = StorageSetting.Deserialize(json);

                    if (setting == null)
                    {
                        setting = new StorageSetting();
                    }
                    setting.ProductBarCodeLength = productBarCodeLength;
                    setting.SertificateBarCodeLength = sertificateBarCodeLength;

                    AppSettingsManager.SetParameterFile("StorageSetting", setting.Serialize());
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
