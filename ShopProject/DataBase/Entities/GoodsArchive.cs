using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    public class GoodsArchive
    {
        [Key]
        [ForeignKey("goods")]
        public Guid id { get; set; }
        /// <summary>
        /// товар якй архівовано
        /// </summary>
        public Goods? goods { get; set; }
        /// <summary>
        /// дата архівації
        /// </summary>
        public DateTimeOffset? createdAt { get; set; }
    }
}
