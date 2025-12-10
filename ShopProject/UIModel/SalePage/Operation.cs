using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.SalePage
{
    public class Operation
    { 
        public int ID { get; set; }
        /// <summary>
        /// тип оплати
        /// </summary>
        public TypePayment TypePayment { get; set; }
        /// <summary>
        /// Тип операції
        /// </summary>
        public TypeOperation TypeOperation { get; set; }
        /// <summary>
        /// сума  що вносить користувачі
        /// </summary>
        public decimal BuyersAmount { get; set; } = decimal.Zero;
        /// <summary>
        /// решта 
        /// </summary>
        public decimal RestPayment { get; set; } = decimal.Zero;
        /// <summary>
        /// загальна сума чеку
        /// </summary>
        public decimal TotalPayment { get; set; } = decimal.Zero;
        /// <summary>
        /// номер фіксального чеку
        /// </summary>
        public string NumberPayment { get; set; } = string.Empty;
        /// <summary>
        /// податок на товари в чеку 0 - без податку 
        /// </summary>
        public string GoodsTax { get; set; } = string.Empty;
        /// <summary>
        /// сума отриманих коштів (службове внесення)
        /// </summary>
        public decimal AmountOfFundsReceived { get; set; } = decimal.Zero;
        /// <summary>
        /// сума отриманих коштів (службова видача)
        /// </summary>
        public decimal AmountOfIssuedFunds { get; set; } = decimal.Zero;
        /// <summary>
        /// код підтвердження на сервері
        /// </summary>
        [Required]
        public MediaAccessControl? MAC { get; set; }
        /// <summary>
        /// час створення чеку
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// знижка на чек
        /// </summary>
        public Discount? Discount { get; set; }
        /// <summary>
        /// Змінна під час якої була операція
        /// </summary>
        public WorkingShift? Shift { get; set; }
        /// <summary>
        /// Список товару який належмть до операції 
        /// </summary>
        public ICollection<Order>? Order { get; set; }
    }
}
