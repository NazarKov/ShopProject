using ShopProject.Core.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.Discount
{
    public class DiscountModel : Model<DiscountModel>
    {
        public int ID { get; set; }
        public string NameDiscount { get; set; } = string.Empty;
        public decimal Rebate { get; set; } = decimal.Zero;
        public decimal TypeDiscount { get; set; } = decimal.Zero;
        public decimal InterimAmount { get; set; } = decimal.Zero;
        public decimal TotalDiscount { get; set; } = decimal.Zero;
        public DateTime CreateAt { get; set; }
        public DateTime FinishedAt { get; set; } 
        public string StringDiscount {
            get
            {
                if (this.TypeDiscount == 0)
                {
                    return TotalDiscount.ToString();
                }
                else if (this.TypeDiscount == 1) 
                { 
                    return TotalDiscount.ToString("N2");
                }
                return string.Empty;
            }  
        } 
    }
}
