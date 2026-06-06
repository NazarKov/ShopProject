using ShopProjectWebServer.Models.Domain.Enum;

namespace ShopProjectWebServer.Models.Domain.Product
{
    public class Product
    { 
        public Guid ID { get; set; } 
        public string Code { get; set; } = string.Empty; 
        public string NameProduct { get; set; } = string.Empty; 
        public string Articule { get; set; } = string.Empty; 
        public decimal Price { get; set; } = decimal.Zero; 
        public decimal Count { get; set; } = decimal.Zero; 
        public ShopProjectWebServer.Models.Domain.Discount.Discount? Discount { get; set; } 
        public TypeStatusProduct Status { get; set; } 
        public DateTimeOffset? CreatedAt { get; set; } 
        public DateTimeOffset? ArhivedAt { get; set; } 
        public DateTimeOffset? OutStockAt { get; set; } 
        public ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit? Unit { get; set; } 
        public ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED? CodeUKTZED { get; set; }  
    }
}
