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
        /// ціна продукту
        /// </summary>
        public double? price { get; set; }
        /// <summary>
        /// ціна з націнкою 
        /// </summary>
        public double? startingPrise { get; set; }
        /// <summary>
        ///кількість товару
        /// </summary>
        public int? count { get; set; }
        /// <summary>
        /// одиниці виміру
        /// </summary>
        public string? units { get; set; }
        /// <summary>
        /// націнка
        /// </summary>
        public double? markUp { get; set; }
        /// <summary>
        /// скідка
        /// </summary>
        public double? sales { get; set; }
        /// <summary>
        /// ключ поєднання
        /// </summary>
        public DateTimeOffset? created_at { get; set; }

        private ICollection<ProductOrder> productOrders { get; set; }
        private ICollection<Archive> archives { get; set; }
        public Product()
        {
            this.productOrders = new List<ProductOrder>();
            this.archives = new List<Archive>();
        }
    }
}
