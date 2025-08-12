using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ShopProjectSQLDataBase.Entities
{
    [Table("UserSignatureKey")]
    public class ElectronicSignatureKey
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }     
        /// <summary>
        /// ключ в форматі byte
        /// </summary>
        public byte[]? Signature { get; set; }
        /// <summary>
        /// пароль до ключа
        /// </summary>
        public string? SignaturePassword { get; set; }
        /// <summary>
        /// користувач ключа
        /// </summary>
        public ICollection<UserEntity>? User { get; set; }
        /// <summary>
        /// створенна ключа
        /// </summary>
        public DateTimeOffset CreateAt { get; set; }
        /// <summary>
        /// дата закінчення ключа
        /// </summary>
        public DateTimeOffset EndAt { get; set; }
    }
}
