using ShopProject.Model.Domain.Discount;
using ShopProject.Model.Domain.PorductCodeUKTZED;
using ShopProject.Model.Domain.ProductUnit;
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
        public Discount.Discount? Discount { get; set; }
        public TypeStatusProduct Status { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? ArhivedAt { get; set; }
        public DateTimeOffset? OutStockAt { get; set; }
        public ProductUnit.ProductUnit? Unit { get; set; }
        public ProductCodeUKTZED? CodeUKTZED { get; set; }
    }
}
