using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopProject.DataBase.Model;

namespace ShopProject.DataBase.Entities
{
    [Table("OperationsRecorderAndUser")]
    public class OperationsRecorderUserEntiti
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        public UserEntiti? Users { get; set; }
        public OperationsRecorderEntiti? OpertionsRecorders { get; set; }

    }
}
