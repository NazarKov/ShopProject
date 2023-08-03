using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    public class Goods
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        /// <summary>
        /// Штрих код товару
        /// </summary>
        public string? code { get; set; }
        /// <summary>
        /// назва товару
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// ариткуль товару
        /// </summary>
        public string? articule { get; set; }
        /// <summary>
        /// ціна товару
        /// </summary>
        public decimal? price { get; set; }
        /// <summary>
        /// кількість товару
        /// </summary>
        public decimal? count { get; set; }
        /// <summary>
        /// знижка
        /// </summary>
        public double? sales { get; set; }
        /// <summary>
        /// статус товару
        /// </summary>
        public string? status { get; set; }
        /// <summary>
        /// дата створення товару
        /// </summary>
        public DateTimeOffset? createdAt { get; set; }
        /// <summary>
        /// архівований товар
        /// </summary>
        public GoodsArchive productArchive { get; set; }
        /// <summary>
        /// товар який закінчився 
        /// </summary>
        public GoodsOutOfStock productsOutStock { get; set; }
        /// <summary>
        /// одиниці
        /// </summary>
        public GoodsUnit unit { get; set; }
        /// <summary>
        /// код УКТЗЕД
        /// </summary>
        public CodeUKTZED codeUKTZED { get; set; }
        /// <summary>
        /// проміжна таблиця
        /// </summary>
        public ICollection<GoodsOperation> orderItem { get; set; }
        
    }
}
