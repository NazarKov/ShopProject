using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Bcpg;

namespace ShopProject.DataBase.Model
{
    public class Operation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// фіксальний номер рро
        /// </summary>
        public string fiscalNumberRRO { get; set; }
        /// <summary>
        /// податковий номер
        /// </summary>
        public string taxNumber { get;set; }
        /// <summary>
        /// Заводьській номер рро
        /// </summary>
        public string factoryNumberRRO { get; set; }
        /// <summary>
        /// індифікатор пакету даних
        /// </summary>
        public decimal dataPacketIdentifier { get;set; }
        /// <summary>
        /// Тип рро
        /// </summary>
        public decimal typeRRO { get; set; }
        /// <summary>
        /// версія пакету даних = 1
        /// </summary>
        public decimal versionDataPaket { get; set; }
        /// <summary>
        /// тип чеку 0 - чек продажі 108 - відкриття зміни 
        /// </summary>
        public decimal typeOperation { get; set; }
        /// <summary>
        /// форма оплати 0 - готівка ,не 0 - безготівка
        /// </summary>
        public decimal formOfPayment { get; set; }
        /// <summary>
        /// сума оплати що вноситься касиром
        /// </summary>
        public decimal buyersAmount { get; set; }
        /// <summary>
        /// решта 
        /// </summary>
        public decimal restPayment { get; set; }
        /// <summary>
        /// номер фіксального чеку
        /// </summary>
        public string numberPayment { get; set; }
        /// <summary>
        /// загальна сума чеку
        /// </summary>
        public decimal totalPayment { get; set; }
        /// <summary>
        /// податок на товари в чеку 0 - без податку 
        /// </summary>
        public string goodsTax { get; set; }
        /// <summary>
        /// кількість чеків продажу
        /// </summary>
        public decimal numberOfSalesReceipts { get; set; }
        /// <summary>
        /// код підтвердження на сервері
        /// </summary>
        public string mac { get; set; }
        /// <summary>
        /// час створення чеку
        /// </summary>
        public DateTime createdAt { get; set; }
        /// <summary>
        /// користувач
        /// </summary>
        public User? user { get; set; }


        public ICollection<GoodsOperation> orderItem { get; set; }

    }
}
