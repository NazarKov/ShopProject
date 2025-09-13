 
namespace ShopProjectWebServer.Api.DtoModels.WorkingShift
{
    public class UpdateWorkingShiftDto
    {
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
        public TypeWokingShiftDto TypeShiftCrateAt { get; set; }
        /// <summary>
        /// позначка про закриття зміної
        /// </summary>
        public TypeWokingShiftDto TypeShiftEndAt { get; set; }
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
        public int MACCreateAtID { get; set; }
        /// <summary>
        /// hesh закриття зміни
        /// </summary> 
        public int MACEndAtID { get; set; }  
        /// <summary>
        /// користувач який відкрив зміну
        /// </summary>
        public Guid UserOpenShiftID { get; set; }
        /// <summary>
        /// користувач який закрив зміну
        /// </summary>
        public Guid UserCloseShiftID { get; set; } 
    }
}
