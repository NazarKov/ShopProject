using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Discount
{
    public class CreateDiscountDto
    {
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
