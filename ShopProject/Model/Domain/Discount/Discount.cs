using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.Discount
{
    public class Discount
    {
        public int ID { get; set; }
        public string NameDiscount { get; set; } = string.Empty; 
        public decimal Rebate { get; set; } = decimal.Zero; 
        public decimal TypeDiscount { get; set; } = decimal.Zero; 
        public decimal InterimAmount { get; set; } = decimal.Zero; 
        public decimal TotalDiscount { get; set; } = decimal.Zero; 
        public DateTime CreateAt { get; set; } 
        public DateTime FinishedAt { get; set; }
    }
}
