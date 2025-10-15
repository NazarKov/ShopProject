using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Product
{
    public class CreateProductDto
    {
        [JsonPropertyName("Code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("NameProduct")]
        public string NameProduct { get; set; } = string.Empty;
        [JsonPropertyName("Articule")]
        public string Articule { get; set; } = string.Empty;
        [JsonPropertyName("Price")]
        public decimal Price { get; set; } = decimal.Zero;
        [JsonPropertyName("Count")]
        public decimal Count { get; set; } = decimal.Zero;
        [JsonPropertyName("Discount_ID")]
        public int Discount_ID { get; set; }
        [JsonPropertyName("Status")]
        public int Status { get; set; }
        [JsonPropertyName("CreatedAt")]
        public DateTimeOffset? CreatedAt { get; set; }
        [JsonPropertyName("ArhivedAt")]
        public DateTimeOffset? ArhivedAt { get; set; }
        [JsonPropertyName("OutStockAt")]
        public DateTimeOffset? OutStockAt { get; set; }
        [JsonPropertyName("Unit_ID")]
        public int Unit_ID { get; set; }
        [JsonPropertyName("CodeUKTZED_ID")]
        public int CodeUKTZED_ID { get; set; }
    }
}
