
using ShopProjectWebServer.Models.Domain.Enum;

namespace ShopProjectWebServer.Models.Domain.Operation
{
    public class Operation
    { 
        public int ID { get; set; } 
        public string FiscalServerId { get; set; } = string.Empty; 
        public TypePayment TypePayment { get; set; } 
        public TypeOperation TypeOperation { get; set; } 
        public decimal BuyersAmount { get; set; } = decimal.Zero; 
        public decimal RestPayment { get; set; } = decimal.Zero; 
        public decimal TotalPayment { get; set; } = decimal.Zero; 
        public string NumberPayment { get; set; } = string.Empty; 
        public string GoodsTax { get; set; } = string.Empty; 
        public int? MACId { get; set; }
        public ShopProjectWebServer.Models.Domain.MediaAccessControl.MediaAccessControl? MAC { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public ShopProjectWebServer.Models.Domain.Discount.Discount? Discount { get; set; } 
        public ShopProjectWebServer.Models.Domain.WorkingShift.WorkingShift? Shift { get; set; } 
    }
}
