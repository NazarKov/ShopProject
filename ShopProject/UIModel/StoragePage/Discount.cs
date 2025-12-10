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
        /// <summary>
        /// відсоток знижки або сума знижки залежить від типу знижки 
        /// </summary>
        public decimal Rebate { get; set; } = decimal.Zero;
        /// <summary>
        /// тип знижки
        /// </summary>
        public decimal TypeDiscount { get; set; } = decimal.Zero;
        /// <summary>
        /// проміжна сума на яку нараховується знижка
        /// </summary>
        public decimal InterimAmount { get; set; } = decimal.Zero;
        /// <summary>
        /// загальна сума знижки
        /// </summary>
        public decimal TotalDiscount { get; set; } = decimal.Zero;
        /// <summary>
        /// дата створення знижки
        /// </summary>
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// дата завершення знижки
        /// </summary>
        public DateTime FinishedAt { get; set; }

    }
}