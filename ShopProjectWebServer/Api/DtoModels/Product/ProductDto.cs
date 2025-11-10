using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
<<<<<<< HEAD
using System.Text.Json.Serialization;
=======
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

namespace ShopProjectWebServer.Api.DtoModels.Product
{
    public class ProductDto
    {
<<<<<<< HEAD
        [JsonPropertyName("ID")]
        public string ID { get; set; }
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
=======
        public string Code { get; set; } = string.Empty; 
        public string NameProduct { get; set; } = string.Empty; 
        public string Articule { get; set; } = string.Empty; 
        public decimal Price { get; set; } = decimal.Zero; 
        public decimal Count { get; set; } = decimal.Zero; 
        public int? Discount_ID { get; set; } 
        public int Status { get; set; } 
        public DateTimeOffset? CreatedAt { get; set; } 
        public DateTimeOffset? ArhivedAt { get; set; } 
        public DateTimeOffset? OutStockAt { get; set; } 
        public int? Unit_ID { get; set; } 
        public int? CodeUKTZED_ID { get; set; }
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
    }
}
