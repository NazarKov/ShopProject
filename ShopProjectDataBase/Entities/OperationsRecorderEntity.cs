using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopProjectSQLDataBase.Helper;

namespace ShopProjectSQLDataBase.Entities
{
    [Table("OperationsRecorder")]
    public class OperationsRecorderEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        /// <summary>
        /// Фіскальний номер ПРРО
        /// </summary>
        public string FiscalNumber { get; set; } = string.Empty;
        /// <summary>
        /// Локальний номер ПРРО
        /// </summary>
        public string LocalNumber { get; set; } = string.Empty;
        /// <summary>
        /// Найменування ПРРО
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Статус ПРРО
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// тип статусу для програми
        /// </summary>
        public TypeStatusOperationRecorder TypeStatus {  get; set; }
        /// <summary>
        /// Дата реєстрації документа
        /// </summary>
        public DateTimeOffset D_REG { get; set; }
        /// <summary>
        /// Адреса господарської одиниці, де використовується ПРРО
        /// </summary>
        public string Address { get; set; } = string.Empty;

        public ObjectOwnerEntity? ObjectOwner { get; set; }

    }
}
