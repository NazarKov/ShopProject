using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    [Table("Units")]
    public class ProductUnitEntiti
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
        public bool isFavorite { get; set; }
        public ICollection<ProductEntiti>? Goods { get; set;}
    }
}
