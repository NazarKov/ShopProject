using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    internal class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
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
        public double? price { get; set; }
        /// <summary>
        /// кількість товару
        /// </summary>
        public int? count { get; set; }
        /// <summary>
        /// одиниці виміру ддя товару
        /// </summary>
        public string? units { get; set; }
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
        public DateTimeOffset? created_at { get; set; }

        public ProductArchive ProductArchive { get; set; }
        public ProductsOutOfStock productsOutStock { get; set; }

        public ICollection<ProductOrder> OrderItem { get; set; }
        
    }
}
