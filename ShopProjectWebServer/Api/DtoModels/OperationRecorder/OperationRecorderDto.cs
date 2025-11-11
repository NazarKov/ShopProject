using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Text.Json.Serialization; 

namespace ShopProjectWebServer.Api.DtoModels.OperationRecorder
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
