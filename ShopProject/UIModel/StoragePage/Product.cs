using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.StoragePage
{
    public class Product
    {
        public Guid ID { get; set; } 
        public string Code { get; set; } = string.Empty; 
        public string NameProduct { get; set; } = string.Empty; 
        public string Articule { get; set; } = string.Empty; 
        public decimal Price { get; set; } = decimal.Zero; 
        public decimal Count { get; set; } = decimal.Zero; 
        public Discount? Discount { get; set; } 
        public TypeStatusProduct Status { get; set; } 
        public DateTimeOffset? CreatedAt { get; set; } 
        public DateTimeOffset? ArhivedAt { get; set; } 
        public DateTimeOffset? OutStockAt { get; set; } 
        public ProductUnit? Unit { get; set; } 
        public ProductCodeUKTZED? CodeUKTZED { get; set; }  
    }
}
