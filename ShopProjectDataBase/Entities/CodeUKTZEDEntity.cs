﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProjectDataBase.DataBase.Model
{
    [Table("CodeUKTZED")]
    public class CodeUKTZEDEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// назва коду УКТЗЕД
        /// </summary>
        public string NameCode { get; set; } = string.Empty;
        /// <summary>
        /// код УКТЗЕД
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// вибрані коди користувачем
        /// </summary>
        public bool isFavorite { get; set; }
    }
}
