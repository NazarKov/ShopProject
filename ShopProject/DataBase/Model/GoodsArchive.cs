using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    internal class GoodsArchive
    {
        [Key]
        [ForeignKey("Product")]
        public int id { get; set; }
        /// <summary>
        /// товар якй архівовано
        /// </summary>
        public Goods? product { get; set; }
        /// <summary>
        /// дата архівації
        /// </summary>
        public DateTimeOffset? createdAt { get; set; }
    }
}
