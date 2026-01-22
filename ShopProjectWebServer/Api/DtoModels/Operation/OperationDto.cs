using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.Operation
{
    public class OperationDto
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("TypePayment")]
        public int TypePayment { get; set; }
        [JsonPropertyName("TypeOperation")]
        public int TypeOperation { get; set; }
        [JsonPropertyName("BuyersAmount")]
        public decimal BuyersAmount { get; set; } = decimal.Zero;
        [JsonPropertyName("RestPayment")]
        public decimal RestPayment { get; set; } = decimal.Zero;
        [JsonPropertyName("TotalPayment")]
        public decimal TotalPayment { get; set; } = decimal.Zero;
        [JsonPropertyName("NumberPayment")]
        public string NumberPayment { get; set; } = string.Empty;
        [JsonPropertyName("GoodsTax")]
        public string GoodsTax { get; set; } = string.Empty; 
        [JsonPropertyName("FiscalServerId")]
        public string FiscalServerId { get; set; } = string.Empty;
        [JsonPropertyName("MAC")]
        public MediaAccessControlDto? MAC { get; set; }
        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("Discount")]
        public int DiscountID { get; set; }
    }
}
