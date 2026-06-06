using ShopProject.Model.Enum;
using System;

namespace ShopProject.Model.Domain.Product
{
    public class Product
    {
        public Guid ID { get; set; }
        public string Code { get; set; } = string.Empty;
        public string NameProduct { get; set; } = string.Empty;
        public string Articule { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public decimal Count { get; set; } = decimal.Zero;
        public TypeStatusProduct Status { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? ArhivedAt { get; set; }
        public DateTimeOffset? OutStockAt { get; set; }
        public ShopProject.Model.Domain.Discount.Discount? Discount { get; set; }
        public ShopProject.Model.Domain.ProductUnit.ProductUnit? Unit { get; set; }
        public ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED? CodeUKTZED { get; set; }
    }
}
