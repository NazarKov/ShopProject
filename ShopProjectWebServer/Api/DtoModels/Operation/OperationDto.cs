using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.DtoModels.Operation
{
    public class OperationDto
    {
        public int ID { get; set; } 
        public int TypePayment { get; set; } 
        public int TypeOperation { get; set; } 
        public decimal BuyersAmount { get; set; } = decimal.Zero; 
        public decimal RestPayment { get; set; } = decimal.Zero; 
        public decimal TotalPayment { get; set; } = decimal.Zero; 
        public string NumberPayment { get; set; } = string.Empty; 
        public string GoodsTax { get; set; } = string.Empty; 
        public decimal AmountOfFundsReceived { get; set; } = decimal.Zero;  
        public int? MACId { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public decimal Discount { get; set; } = decimal.Zero;  
    }
}
