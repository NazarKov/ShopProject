using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    [Table("Operation")]
    public class OperationEntiti
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// фіксальний номер рро
        /// </summary>
        public string FiscalNumberRRO { get; set; } = string.Empty;
        /// <summary>
        /// податковий номер
        /// </summary>
        public string TaxNumber { get; set; } = string.Empty;
        /// <summary>
        /// Заводьській номер рро
        /// </summary>
        public string FactoryNumberRRO { get; set; } = string.Empty;
        /// <summary>
        /// індифікатор пакету даних
        /// </summary>
        public decimal DataPacketIdentifier { get;set; } = decimal.Zero;
        /// <summary>
        /// Тип рро
        /// </summary>
        public decimal TypeRRO { get; set; } = decimal.Zero;
        /// <summary>
        /// версія пакету даних = 1
        /// </summary>
        public decimal VersionDataPaket { get; set; } = decimal.Zero;
        /// <summary>
        /// тип чеку 0 - чек продажі 108 - відкриття зміни  100 - 200 фіксальний чек 200 - 300 не фіксальний чек   
        /// </summary>
        public decimal TypeOperation { get; set; } = decimal.Zero;
        /// <summary>
        /// форма оплати 0 - готівка ,не 0 - безготівка
        /// </summary>
        public decimal FormOfPayment { get; set; } = decimal.Zero;
        /// <summary>
        /// сума оплати що вноситься касиром
        /// </summary>
        public decimal BuyersAmount { get; set; } = decimal.Zero;
        /// <summary>
        /// решта 
        /// </summary>
        public decimal RestPayment { get; set; } = decimal.Zero;
        /// <summary>
        /// номер фіксального чеку
        /// </summary>
        public string NumberPayment { get; set; } = string.Empty;
        /// <summary>
        /// загальна сума чеку
        /// </summary>
        public decimal TotalPayment { get; set; } = decimal.Zero;
        /// <summary>
        /// податок на товари в чеку 0 - без податку 
        /// </summary>
        public string GoodsTax { get; set; } = string.Empty;
        /// <summary>
        /// кількість чеків продажу
        /// </summary>
        public decimal NumberOfSalesReceipts { get; set; } = decimal.Zero;
        /// <summary>
        /// кількість чеків повернення
        /// </summary>
        public decimal NumberOfPendingReturns { get; set; } = decimal.Zero;
        /// <summary>
        /// сума отриманих коштів (службове внесення)
        /// </summary>
        public decimal AmountOfFundsReceived { get; set; } = decimal.Zero;
        /// <summary>
        /// сума отриманих коштів (службова видача)
        /// </summary>
        public decimal AmountOfIssuedFunds { get; set; } = decimal.Zero;
        /// <summary>
        /// сума отриманих коштів готівка
        /// </summary>
        public decimal AmountReceivedCash { get; set; } = decimal.Zero;
        /// <summary>
        /// сума виданих коштів готівка
        /// </summary>
        public decimal AmountIssuedCash { get; set; } = decimal.Zero;
        /// <summary>
        /// сума отриманих коштів картка
        /// </summary>
        public decimal AmountReceivedCard { get; set; } = decimal.Zero;
        /// <summary>
        /// сума виданих коштів картка
        /// </summary>
        public decimal AmountIssuedCard { get; set; } = decimal.Zero;
        /// <summary>
        /// сума чеків повернення готівка
        /// </summary>
        public decimal AmountCheckReturnCash { get; set; } = decimal.Zero;
        /// <summary>
        /// сума чеків повернення картка
        /// </summary>
        public decimal AmountCheckReturnCard { get; set; } = decimal.Zero;
        /// <summary>
        /// код підтвердження на сервері
        /// </summary>
        public string MAC { get; set; } = string.Empty;
        /// <summary>
        /// час створення чеку
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// користувач
        /// </summary>
        public UserEntiti? User { get; set; }


        public ICollection<OrderEntiti>? Order { get; set; }

    }
}
