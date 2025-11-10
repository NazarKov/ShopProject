using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
<<<<<<< HEAD
using System.Text.Json.Serialization;
=======
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

namespace ShopProjectWebServer.Api.DtoModels.OperationRecorder
{
    public class OperationRecorderDto
<<<<<<< HEAD
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
=======
    { 
        public string FiscalNumber { get; set; } = string.Empty; 
        public string LocalNumber { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty; 
        public int TypeStatus { get; set; } 
        public DateTimeOffset D_REG { get; set; } 
        public string Address { get; set; } = string.Empty; 
        public Guid? ObjectOwner_ID { get; set; } 
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
    }
}
