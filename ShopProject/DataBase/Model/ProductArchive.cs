using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    internal class ProductArchive
    {
        [Key]
        [ForeignKey("Product")]
        public int ID { get; set; }
        public Product? Product { get; set; }
        public DateTimeOffset? created_at { get; set; }
    }
}
