using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.WorkingShift
{
    public class CreateWorkingShiftDto
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("FiscalNumberRRO")]
        public string FiscalNumberRRO { get; set; } = string.Empty;
        [JsonPropertyName("FactoryNumberRRO")]
        public string FactoryNumberRRO { get; set; } = string.Empty;
        [JsonPropertyName("DataPacketIdentifier")]
        public decimal DataPacketIdentifier { get; set; } = decimal.Zero;
        [JsonPropertyName("TypeRRO")]
        public decimal TypeRRO { get; set; } = decimal.Zero;
        [JsonPropertyName("TypeShiftCrateAt")]
        public int TypeShiftCrateAt { get; set; }
        [JsonPropertyName("MACCreateAtID")]
        public int MACCreateAtID { get; set; }
        [JsonPropertyName("UserOpenShiftID")]
        public string UserOpenShiftID { get; set; }
    }
}
