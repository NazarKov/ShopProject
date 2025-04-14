using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDataBase.DataBase.Entities
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
        /// відсоток знижки
        /// </summary>
        public int PercentageDiscount { get; set; } = 0;
        /// <summary>
        /// опис знижки
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// дата створення знижки
        /// </summary>
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// дата завершення знижки
        /// </summary>
        public DateTime FinishedAt { get; set; }
        /// <summary>
        /// тип знижки  0 - знижка на чек , 1 - знижка на товар
        /// </summary>
        public int TypeDiscount { get; set; }

    }
}
