using ShopProjectWebServer.Models.Domain.Enum; 

namespace ShopProjectWebServer.Models.Domain.OperationRecorder
{
    public class OperationRecorder
    { 
        public Guid ID { get; set; } 
        public string FiscalNumber { get; set; } = string.Empty; 
        public string LocalNumber { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty; 
        public TypeStatusOperationRecorder TypeStatus { get; set; } 
        public DateTimeOffset D_REG { get; set; } 
        public string Address { get; set; } = string.Empty;

        public ShopProjectWebServer.Models.Domain.TaxObject.TaxObject? TaxObject;
    }
}
