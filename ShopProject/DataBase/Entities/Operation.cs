using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    public class Operation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// сума операції
        /// </summary>
        public double sum { get; set; }
        /// <summary>
        ///внесена сума користувачем 
        /// </summary>
        public double buyersAmount { get; set; }
        /// <summary>
        /// решта
        /// </summary>
        public double rest { get; set; }
        /// <summary>
        /// знижка
        /// </summary>
        public double discount { get; set; }
        /// <summary>
        /// статус операції
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// локальний номер за день
        /// </summary>
        public string localNumber { get; set; }
        /// <summary>
        /// тип оплати
        /// </summary>
        public string typeOplat { get; set; }
        /// <summary>
        /// час створення операції
        /// </summary>
        public DateTimeOffset? createdAt { get; set; }
        /// <summary>
        /// користувач
        /// </summary>
        public User? user { get; set; }
        /// <summary>
        /// номер пристрою розрахункових операцій якій проводить операцію
        /// </summary>
        public string deviceSettlementOperationsNumber { get; set; }

        public ICollection<GoodsOperation> orderItem { get; set; }

    }
}
