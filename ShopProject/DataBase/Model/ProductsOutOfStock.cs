using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.Model
{
    internal class ProductsOutOfStock
    {
        [Key]
        [ForeignKey("Product")]
        public int ID { get; set; }
        public Product? Product { get; set; }
        public DateTimeOffset? created_at { get; set; }
    }
}
