using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDataBase.Entities
{
    [Table("Discount")]
    public class DiscountEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// назва знижки
        /// </summary>
        public string NameDiscount { get; set; } = string.Empty;
        /// <summary>
        /// відсоток знижки або сума знижки залежить від типу знижки 
        /// </summary>
        public decimal Discount { get; set; } = decimal.Zero;
        /// <summary>
        /// тип знижки
        /// </summary>
        public decimal TypeDiscount { get; set; } = decimal.Zero;
        /// <summary>
        /// проміжна сума на яку нараховується знижка
        /// </summary>
        public decimal InterimAmount {  get; set; } = decimal.Zero;
        /// <summary>
        /// загальна сума знижки
        /// </summary>
        public decimal TotalDiscount {  get; set; } = decimal.Zero;
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
