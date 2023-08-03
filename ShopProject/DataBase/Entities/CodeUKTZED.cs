using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.Model
{
    public class CodeUKTZED
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// назва коду УКТЗЕД
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// код УКТЗЕД
        /// </summary>
        public string code { get; set; }

        public ICollection<Goods> goods { get; set; }
    }
}
