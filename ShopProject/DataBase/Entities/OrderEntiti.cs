using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    [Table("Order")]
    public class OrderEntiti
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// кількість товару
        /// </summary>
        public int Count { get; set; } = 0;
        /// <summary>
        /// товар
        /// </summary>
        public  ProductEntiti? Goods { get; set; }
        /// <summary>
        /// операція до якої належить товар
        /// </summary>
        public virtual OperationEntiti? Operation { get; set; }
    }
}
