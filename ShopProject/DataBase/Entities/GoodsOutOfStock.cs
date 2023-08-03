using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.Model
{
    public class GoodsOutOfStock
    {
        [Key]
        [ForeignKey("goods")]
        public Guid id { get; set; }
        /// <summary>
        /// товар який закінчився
        /// </summary>
        public Goods? goods { get; set; }
        /// <summary>
        /// дата коли товар закінчився
        /// </summary>
        public DateTimeOffset? createdAt { get; set; }
    }
}
