using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    internal class GoodsOperation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// кількість
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// товар
        /// </summary>
        public  Goods product { get; set; }
        /// <summary>
        /// операція до якої належить товар
        /// </summary>
        public virtual Operation operation { get; set; }
    }
}
