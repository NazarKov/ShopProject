using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopProjectDataBase.DataBase.Model;

namespace ShopProjectDataBase.DataBase.Entities
{
    [Table("OperationsRecorderAndUser")]
    public class OperationsRecorderUserEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        public UserEntity? Users { get; set; }
        public OperationsRecorderEntity? OpertionsRecorders { get; set; }

    }
}
