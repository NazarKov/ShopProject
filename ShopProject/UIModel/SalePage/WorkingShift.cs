using ShopProject.UIModel.UserPage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.UIModel.SalePage
{   public class WorkingShift
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// фіксальний номер рро
        /// </summary>
        public string FiscalNumberRRO { get; set; } = string.Empty;
        /// <summary>
        /// Заводьській номер рро
        /// </summary>
        public string FactoryNumberRRO { get; set; } = string.Empty;
        /// <summary>
        /// індифікатор пакету даних
        /// </summary>
        public decimal DataPacketIdentifier { get; set; } = decimal.Zero;
        /// <summary>
        /// Тип рро
        /// </summary>
        public decimal TypeRRO { get; set; } = decimal.Zero;
        /// <summary>
        /// позначка про відкриття зміної
        /// </summary>
        public TypeWorkingShift TypeShiftCrateAt { get; set; }
        /// <summary>
        /// позначка про закриття зміної
        /// </summary>
        public TypeWorkingShift TypeShiftEndAt { get; set; }
        /// <summary>
        /// Загальна кільскість чеків за зміну
        /// </summary>
        public decimal TotalCheckForShift { get; set; } = decimal.Zero;
        /// <summary>
        /// Загальна кільскість чеків повернення за зміну
        /// </summary>
        public decimal TotalReturnCheckForShift { get; set; } = decimal.Zero;
        /// <summary>
        /// Загальна сума службових внесень коштів  готівка
        /// </summary>
        public decimal AmountOfOfficialFundsReceivedCash { get; set; } = decimal.Zero;
        /// <summary>
        /// Загальна сума службрвих видач коштів готівка
        /// </summary>
        public decimal AmountOfOfficialFundsIssuedCash { get; set; } = decimal.Zero;
        /// <summary>
        /// Загальна сума службових внесень коштів  карта
        /// </summary>
        public decimal AmountOfOfficialFundsReceivedCard { get; set; } = decimal.Zero;
        /// <summary>
        /// Загальна сума службрвих видач коштів карта
        /// </summary>
        public decimal AmountOfOfficialFundsIssuedCard { get; set; } = decimal.Zero;
        /// <summary>
        /// Загальна сума чеків за зміну 
        /// </summary>
        public decimal AmountOfFundsReceived { get; set; } = decimal.Zero;
        /// <summary>
        /// Загальна сума решти за зміну 
        /// </summary>
        public decimal AmountOfFundsIssued { get; set; } = decimal.Zero;
        /// <summary>
        /// hesh відкриття зміни
        /// </summary>
        [JsonIgnore]
        public MediaAccessControl MACCreateAt { get; set; }
        /// <summary>
        /// hesh закриття зміни
        /// </summary> 
        [JsonIgnore]
        public MediaAccessControl MACEndAt { get; set; }
        /// <summary>
        /// час відриття зміної
        /// </summary>
        public DateTimeOffset CreateAt { get; set; }
        /// <summary>
        /// час закриття зміної
        /// </summary>
        public DateTimeOffset EndAt { get; set; }
        /// <summary>
        /// користувач який відкрив зміну
        /// </summary> 
        public User UserOpenShift { get; set; }
        /// <summary>
        /// користувач який закрив зміну
        /// </summary> 
        public User UserCloseShift { get; set; }
        /// <summary>
        /// Список операцій викониних під час зміної
        /// </summary>
        [JsonIgnore]
        public IEnumerable<Operation>? Operations { get; set; }
    }
}
