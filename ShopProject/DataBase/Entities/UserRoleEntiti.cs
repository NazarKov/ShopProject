using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Entities
{
    [Table("UserRole")]
    public class UserRoleEntiti
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// назва посади користувача
        /// </summary>
        public string NameRole { get; set; } = string.Empty;
        /// <summary>
        /// тип доступу користувача до програми 
        /// </summary>
        public int TypeAccess { get; set; } = 0;
    }
}
