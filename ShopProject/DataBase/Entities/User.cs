using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ShopProject.DataBase.Model
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// імя користувача
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// прізвище користувача
        /// </summary>
        public string? lastname { get; set; }
        /// <summary>
        /// телефон користувача
        /// </summary>
        public string? phone { get; set; }
        /// <summary>
        /// адрес користувача
        /// </summary>
        public string? addres { get; set; }
        /// <summary>
        /// пароль користувача
        /// </summary>
        public string? password { get; set; }
        /// <summary>
        /// тип доступу до програми
        /// </summary>
        public int? type_d { get; set; }
        /// <summary>
        /// посада користувача
        /// </summary>
        public string? posada { get; set; }
        public DateTimeOffset? created_at { get; set; }
    }
}
