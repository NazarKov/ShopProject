 using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopProjectDataBase.Helper;

namespace ShopProjectDataBase.Entities
{
    [Table("Operation")]
    public class OperationEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public int? MACId { get; set; }  
        public MediaAccessControlEntity? MAC { get; set; } 
        /// <summary>
        /// час створення чеку
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// знижка на чек
        /// </summary>
        public decimal Discount { get; set; } = decimal.Zero;
        /// <summary>
        /// Змінна під час якої була операція
        /// </summary>
        public WorkingShiftEntity? Shift { get; set; }
        /// <summary>
        /// Список товару який належмть до операції 
        /// </summary>
        public ICollection<OrderEntity>? Order { get; set; }

    }
}
