using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    internal class ProductOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// кількість
        /// </summary>
        public int count { get; set; }
        public Product? product { get; set; }
        public Order? order { get; set; }
    }
}
