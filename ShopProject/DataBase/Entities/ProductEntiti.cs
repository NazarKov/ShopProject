using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    [Table("Product")]
    public class ProductEntiti
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        /// <summary>
        /// Штрих код товару
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// назва товару
        /// </summary>
        public string NameProduct { get; set; } = string.Empty;
        /// <summary>
        /// ариткуль товару
        /// </summary>
        public string Articule { get; set; } = string.Empty;
        /// <summary>
        /// ціна товару
        /// </summary>
        public decimal Price { get; set; } = decimal.Zero;
        /// <summary>
        /// кількість товару
        /// </summary>
        public decimal Count { get; set; } = decimal.Zero;
        /// <summary>
        /// знижка
        /// </summary>
        public double? Sales { get; set; }
        /// <summary>
        /// статус товару
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// дата створення товару
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>
        /// архівований товар
        /// </summary>
        public DateTimeOffset? ArhivedAt { get; set; }
        /// <summary>
        /// товар який закінчився 
        /// </summary>
        public DateTimeOffset? OutStockAt { get; set; }
        /// <summary>
        /// одиниці
        /// </summary>
        public ProductUnitEntiti? Unit { get; set; }
        /// <summary>
        /// код УКТЗЕД
        /// </summary>
        public CodeUKTZEDEntiti? CodeUKTZED { get; set; }
        /// <summary>
        /// проміжна таблиця
        /// </summary>
        public ICollection<OrderEntiti>? Order { get; set; }
        
    }
}
