using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.StoragePage
{
    public class Discount
    {
        public int ID { get; set; } 
        public string NameDiscount { get; set; } = string.Empty; 
        public int PercentageDiscount { get; set; } = 0; 
        public string Description { get; set; } = string.Empty; 
        public DateTime CreateAt { get; set; } 
        public DateTime FinishedAt { get; set; } 
        public int TypeDiscount { get; set; }
    }
}
