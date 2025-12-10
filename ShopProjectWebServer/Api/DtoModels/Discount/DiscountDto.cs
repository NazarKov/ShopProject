using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.Discount
{
    public class DiscountDto
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("NameDiscount")]
        public string NameDiscount { get; set; } = string.Empty;
        [JsonPropertyName("Discount")]
        public decimal Discount { get; set; } = decimal.Zero;
        [JsonPropertyName("TypeDiscount")]
        public decimal TypeDiscount { get; set; } = decimal.Zero;
        [JsonPropertyName("InterimAmount")]
        public decimal InterimAmount { get; set; } = decimal.Zero;
        [JsonPropertyName("TotalDiscount")]
        public decimal TotalDiscount { get; set; } = decimal.Zero;
        [JsonPropertyName("CreateAt")]
        public DateTime CreateAt { get; set; }
        [JsonPropertyName("FinishedAt")]
        public DateTime FinishedAt { get; set; }
    }
}
