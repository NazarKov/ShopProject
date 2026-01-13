using ShopProject.UIModel.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.UIModel.SettingPage
{
    public class OperationRecorderSetting
    {
        public bool IsTestMode { get; set; }     
        public string DeleteBarCode { get; set; } =string.Empty;
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
        public static OperationRecorderSetting? Deserialize(string jason)
        {
            if (jason != null && jason != string.Empty)
            {
                return JsonSerializer.Deserialize<OperationRecorderSetting>(jason);
            }
            else
            {
                return null;
            }
        }
    }
}
