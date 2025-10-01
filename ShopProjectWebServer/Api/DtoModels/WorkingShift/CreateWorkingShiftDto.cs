namespace ShopProjectWebServer.Api.DtoModels.WorkingShift
{
    public class CreateWorkingShiftDto
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
        public int TypeShiftCrateAt { get; set; }
        /// <summary>
        /// hesh відкриття зміни
        /// </summary> 
        public int MACCreateAtID { get; set; } 
        /// <summary>
        /// користувач який відкрив зміну
        /// </summary>
        public Guid UserOpenShiftID { get; set; } 
    }
}
