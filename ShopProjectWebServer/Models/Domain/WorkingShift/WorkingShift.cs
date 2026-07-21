using ShopProjectWebServer.Models.Domain.Enum;

namespace ShopProjectWebServer.Models.Domain.WorkingShift
{
    public class WorkingShift
    { 
        public int ID { get; set; } 
        public string FiscalNumberRRO { get; set; } = string.Empty; 
        public string FactoryNumberRRO { get; set; } = string.Empty; 
        public decimal DataPacketIdentifier { get; set; } = decimal.Zero; 
        public decimal TypeRRO { get; set; } = decimal.Zero; 
        public TypeWorkingShift TypeShiftCrateAt { get; set; } 
        public TypeWorkingShift TypeShiftEndAt { get; set; } 
        public decimal TotalCheckForShift { get; set; } = decimal.Zero; 
        public decimal TotalReturnCheckForShift { get; set; } = decimal.Zero; 
        public decimal AmountOfOfficialFundsReceivedCash { get; set; } = decimal.Zero; 
        public decimal AmountOfOfficialFundsIssuedCash { get; set; } = decimal.Zero; 
        public decimal AmountOfOfficialFundsReceivedCard { get; set; } = decimal.Zero; 
        public decimal AmountOfOfficialFundsIssuedCard { get; set; } = decimal.Zero; 
        public decimal AmountOfFundsReceived { get; set; } = decimal.Zero; 
        public decimal AmountOfFundsIssued { get; set; } = decimal.Zero;  
        public ShopProjectWebServer.Models.Domain.MediaAccessControl.MediaAccessControl? MACCreateAt { get; set; }  
        public ShopProjectWebServer.Models.Domain.MediaAccessControl.MediaAccessControl? MACEndAt { get; set; } 
        public DateTimeOffset CreateAt { get; set; } 
        public DateTimeOffset EndAt { get; set; } 
        public ShopProjectWebServer.Models.Domain.User.User? UserOpenShift { get; set; } 
        public ShopProjectWebServer.Models.Domain.User.User? UserCloseShift { get; set; }  
    }
}
