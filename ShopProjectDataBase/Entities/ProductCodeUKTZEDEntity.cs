using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopProjectSQLDataBase.Helper;

namespace ShopProjectSQLDataBase.Entities
{
    [Table("CodeUKTZED")]
    public class ProductCodeUKTZEDEntity
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
        public TypeStatusCodeUKTZED Status { get; set; }
    }
}
