﻿using System;
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
        /// тип чеку 0 - чек продажі 108 - відкриття зміни  100 - 200 фіксальний чек 200 - 300 не фіксальний чек   
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
        /// кількість чеків повернення
        /// </summary>
        public decimal numberOfPendingReturns { get; set; }
        /// <summary>
        /// сума отриманих коштів (службове внесення)
        /// </summary>
        public decimal amountOfFundsReceived { get; set; }
        /// <summary>
        /// сума отриманих коштів (службова видача)
        /// </summary>
        public decimal amountOfIssuedFunds { get; set; }
        /// <summary>
        /// сума отриманих коштів готівка
        /// </summary>
        public decimal amountReceivedCash { get;set; }
        /// <summary>
        /// сума виданих коштів готівка
        /// </summary>
        public decimal amountIssuedCash { get; set; }
        /// <summary>
        /// сума отриманих коштів картка
        /// </summary>
        public decimal amountReceivedCard { get; set; }
        /// <summary>
        /// сума виданих коштів картка
        /// </summary>
        public decimal amountIssuedCard { get; set; }
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
