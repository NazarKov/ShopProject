using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDataBase.Entities
{
    [Table("TaxObjectUser")]
    public class TaxObjectUserEnitity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public UserEntity? User { get; set; }
        public TaxObjectEntity? TaxObject { get; set; }
    }
}
