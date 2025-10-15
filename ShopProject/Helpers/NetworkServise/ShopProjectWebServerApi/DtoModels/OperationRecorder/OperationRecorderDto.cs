using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.OperationRecorder
{
    public class OperationRecorderDto
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("FiscalNumber")]
        public string FiscalNumber { get; set; } = string.Empty;
        [JsonPropertyName("LocalNumber")]
        public string LocalNumber { get; set; } = string.Empty;
        [JsonPropertyName("Name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("Status")]
        public string Status { get; set; } = string.Empty;
        [JsonPropertyName("TypeStatus")]
        public int TypeStatus { get; set; }
        [JsonPropertyName("D_REG")]
        public DateTimeOffset D_REG { get; set; }
        [JsonPropertyName("Address")]
        public string Address { get; set; } = string.Empty;
        [JsonPropertyName("ObjectOwner_ID")]
        public string? ObjectOwner_ID { get; set; }
    }
}
