using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopProjectSQLDataBase.Helper;

namespace ShopProjectDataBase.DataBase.Model
{
    [Table("Units")]
    public class ProductUnitEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// неповна назва одиниці
        /// </summary>
        public string NameUnit { get; set; } = string.Empty;
        /// <summary>
        /// Повна назва одиниці
        /// </summary>
        public string ShortNameUnit { get; set; } = string.Empty;
        /// <summary>
        /// номер одиниці
        /// </summary>
        public int Number { get; set; } = 0;
        /// <summary>
        /// вибрані одиниці користувачем
        /// </summary>
        public TypeStatusUnit Status { get; set; }

    }
}
