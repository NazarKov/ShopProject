using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
<<<<<<< HEAD
using System.Text.Json.Serialization;
=======
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

namespace ShopProjectWebServer.Api.DtoModels.ObjectOwner
{
    public class ObjectOwnerListDto
<<<<<<< HEAD
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("TypeObjectName")]
        public string TypeObjectName { get; set; } = string.Empty;
        [JsonPropertyName("NameObject")]
        public string NameObject { get; set; } = string.Empty;
        [JsonPropertyName("CodeObject")]
        public string CodeObject { get; set; } = string.Empty;
        [JsonPropertyName("Address")]
        public string Address { get; set; } = string.Empty;
        [JsonPropertyName("Status")]
        public string Status { get; set; } = string.Empty;
        [JsonPropertyName("TypeStatus")]
        public int TypeStatus { get; set; }
        [JsonPropertyName("TypeOfRights")]
        public string TypeOfRights { get; set; } = string.Empty;
        [JsonPropertyName("D_ACC_START")]
        public DateTimeOffset? D_ACC_START { get; set; }
        [JsonPropertyName("D_ACC_END")]
        public DateTimeOffset? D_ACC_END { get; set; }
        [JsonPropertyName("C_DISTR")]
        public string C_DISTR { get; set; } = string.Empty;
        [JsonPropertyName("D_LAST_CH")]
        public DateTimeOffset? D_LAST_CH { get; set; }
        [JsonPropertyName("C_TERRIT")]
        public string C_TERRIT { get; set; } = string.Empty;
        [JsonPropertyName("REG_NUM_OBJ")]
        public string? REG_NUM_OBJ { get; set; }
        [JsonPropertyName("KATOTTG")]
        public string KATOTTG { get; set; } = string.Empty;
=======
    { 
        public string TypeObjectName { get; set; } = string.Empty; 
        public string NameObject { get; set; } = string.Empty; 
        public string CodeObject { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty; 
        public int TypeStatus { get; set; } 
        public string TypeOfRights { get; set; } = string.Empty; 
        public DateTimeOffset? D_ACC_START { get; set; } 
        public DateTimeOffset? D_ACC_END { get; set; } 
        public string C_DISTR { get; set; } = string.Empty; 
        public DateTimeOffset? D_LAST_CH { get; set; } 
        public string C_TERRIT { get; set; } = string.Empty; 
        public string? REG_NUM_OBJ { get; set; } 
        public string KATOTTG { get; set; } = string.Empty; 
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
    }
}
