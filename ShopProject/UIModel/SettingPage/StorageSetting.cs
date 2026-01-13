using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.UIModel.SettingPage
{
    internal class StorageSetting
    {
        public int ProductBarCodeLength { get; set; }
        public int SertificateBarCodeLength { get; set; }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
        public static StorageSetting? Deserialize(string jason)
        {
            if (jason != null && jason != string.Empty)
            {
                return JsonSerializer.Deserialize<StorageSetting>(jason);
            }
            else
            {
                return null;
            }
        }
    }
}
