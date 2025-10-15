using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.DtoModels.Product
{
    public class ProductDto
    {
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
    }
}
