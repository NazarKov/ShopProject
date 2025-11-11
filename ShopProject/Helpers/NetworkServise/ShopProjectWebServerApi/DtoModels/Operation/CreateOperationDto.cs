using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Operation
{
    public class CreateOperationDto
    {
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
        [JsonPropertyName("AmountOfFundsReceived")]
        public decimal AmountOfFundsReceived { get; set; } = decimal.Zero;
        [JsonPropertyName("AmountOfIssuedFunds")]
        public decimal AmountOfIssuedFunds { get; set; } = decimal.Zero;
        [JsonPropertyName("MACID")]
        public int MACID { get; set; }
        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("Discount")]
        public decimal Discount { get; set; } = decimal.Zero;
        [JsonPropertyName("ShiftID")]
        public int ShiftID { get; set; }
    }
}
