using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ShopProjectDataBase.DataBase.Entities
{
    [Table("ObjectOwner")]
    public class ObjectOwnerEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        /// <summary>
        /// Тип обєкта оподаткування
        /// </summary>
        public string TypeObjectName { get; set; } = string.Empty;
        /// <summary>
        /// Найменування обєкта оподаткування
        /// </summary>
        public string NameObject { get; set; } = string.Empty;
        /// <summary>
        /// Ідентифікатор об’єкта оподаткування
        /// </summary>
        public string CodeObject { get; set; } = string.Empty;
        /// <summary>
        /// Місцезнаходження обєкта оподаткування
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Стан обєкта оподаткування
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Вид права на об’єкт
        /// </summary>
        public string TypeOfRights { get; set; } = string.Empty;
        /// <summary>
        /// Дата взяття на облік
        /// </summary>
        public DateTimeOffset? D_ACC_START { get; set; }
        /// <summary>
        /// Дата зняття з обліку
        /// </summary>
        public DateTimeOffset? D_ACC_END { get; set; }
        /// <summary>
        /// ДПІ обліку
        /// </summary>
        public string C_DISTR { get; set; } = string.Empty;
        /// <summary>
        /// Дата внесення змін
        /// </summary>
        public DateTimeOffset? D_LAST_CH { get; set; }
        /// <summary>
        /// код КОАТУУ об’єкта оподаткування
        /// </summary>
        public string C_TERRIT { get; set; } = string.Empty;
        /// <summary>
        /// Реєстраційний номер об’єкта оподаткування
        /// </summary>
        public object? REG_NUM_OBJ { get; set; } 
        /// <summary>
        /// Код КАТОТТГ
        /// </summary>
        public string KATOTTG { get; set; } = string.Empty;
        /// <summary>
        /// список привязаних РРО до однієї Точки
        /// </summary>
        public ICollection<OperationsRecorderEntity>? OperationsRecorderEntitis { get; set; }


    }
}
