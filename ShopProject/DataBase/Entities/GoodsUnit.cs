using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.Model
{
    public class GoodsUnit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// неповна назва одиниці
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Повна назва одиниці
        /// </summary>
        public string shortName { get; set; }
        /// <summary>
        /// номер одиниці
        /// </summary>
        public int number { get; set; }

        public ICollection<Goods> goods { get; set;}
    }
}
