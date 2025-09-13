using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ShopProjectDataBase.Entities
{
    [Table("UserToken")]
    public class TokenEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        /// <summary>
        /// користувач
        /// </summary>
        public UserEntity? User { get; set; }
        /// <summary>
        /// токен
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// пристрій з якого була авторизація
        /// </summary>
        public string Device { get; set; } = string.Empty;
        /// <summary>
        /// дата авторизації
        /// </summary>
        public DateTime CreateAt { get; set; }
    }
}
