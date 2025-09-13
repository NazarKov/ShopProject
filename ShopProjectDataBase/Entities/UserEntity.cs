using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopProjectDataBase.Helper;

namespace ShopProjectDataBase.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        /// <summary>
        /// логін користувача
        /// </summary>
        public string Login { get; set; } = string.Empty;
        /// <summary>
        /// пароль користувача
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Прізвище, ім’я та по батькові
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Податковий номер
        /// </summary>
        public string TIN { get; set; } = string.Empty;
        /// <summary>
        /// автоматичний вхід користувача 
        /// </summary>
        public bool AutomaticLogin {get;set;}
        /// <summary>
        /// статус користувача 
        /// </summary>
        public TypeStatusUser Status { get; set; } 
        /// <summary>
        /// тип доступу користувача до програми
        /// </summary>
        public UserRoleEntity? UserRole { get; set; }
        /// <summary>
        /// ключ ЕЦП
        /// </summary>
        public ElectronicSignatureKey? SignatureKey { get; set; }
        /// <summary>
        /// Списко токенів користувача
        /// </summary>
        private ICollection<TokenEntity>? Tokens { get; set; }
        /// <summary>
        /// дата створення користувача
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; } 
    }
}
