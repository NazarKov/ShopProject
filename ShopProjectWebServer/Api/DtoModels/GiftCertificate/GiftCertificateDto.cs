using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.GiftCertificate
{
    public class GiftCertificateDto
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("Code")]
        public string? Code { get; set; }
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
        [JsonPropertyName("Price")]
        public decimal? Price { get; set; }
        [JsonPropertyName("Description")]
        public string? Description { get; set; }
        [JsonPropertyName("Status")]
        public int Status { get; set; } 
        [JsonPropertyName("CreateAt")]
        public DateTime? CreateAt { get; set; } 
    }
}
