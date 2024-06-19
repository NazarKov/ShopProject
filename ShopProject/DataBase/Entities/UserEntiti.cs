using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopProject.DataBase.Entities;

namespace ShopProject.DataBase.Model
{
    [Table("User")]
    public class UserEntiti
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        /// <summary>
        /// логін користувача
        /// </summary>
        public string Login { get; set; } = string.Empty;
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
        /// шлях до ключа ЕЦП
        /// </summary>
        public string KeyPath { get; set; } = string.Empty;
        /// <summary>
        /// пароль до ключа ЕЦП
        /// </summary>
        public string KeyPassword { get; set; } = string.Empty;
        /// <summary>
        /// пароль користувача
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// автоматичний вхід користувача 
        /// </summary>
        public bool AutomaticLogin {get;set;}
        /// <summary>
        /// статус користувача 0 - без електроного ключа рро , 1 - з електроним ключом для рро  
        /// </summary>
        public decimal Status { get; set; } = decimal.Zero;
        /// <summary>
        /// тип доступу користувача до програми
        /// </summary>
        public UserRoleEntiti? UserRole { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }
        
    }
}
