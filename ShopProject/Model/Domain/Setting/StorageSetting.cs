using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.Setting
{
    public class StorageSetting
    {
        public int ProductBarCodeLength { get; set; }
        public int SertificateBarCodeLength { get; set; }
        [JsonIgnore]
        public List<string> CountShowList { get; set; } = new List<string>() { "10", "25", "50", "100", "250", "500", "1000" };
        public int SelectIndexCountShowList { get; set; } = 0;
    }
}
