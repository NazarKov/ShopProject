using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 

namespace ShopProjectDataBase.Entities
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
        public  ProductEntity? Product { get; set; }
        /// <summary>
        /// операція до якої належить товар
        /// </summary>
        public OperationEntity? Operation { get; set; } 
    }
}
