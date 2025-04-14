using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopProjectDataBase.DataBase.Entities;

namespace ShopProjectDataBase.DataBase.Model
{
    [Table("Order")]
    public class OrderEntity
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
        public  ProductEntity? Goods { get; set; }
        /// <summary>
        /// операція до якої належить товар
        /// </summary>
        public OperationEntity? Operation { get; set; }
        /// <summary>
        /// знижка на чек
        /// </summary>
        public DiscountEntity? Discount { get; set; }
    }
}
