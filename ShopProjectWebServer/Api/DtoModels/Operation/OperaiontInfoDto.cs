using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.Operation
{
    public class OperaiontInfoDto
    {
        [JsonPropertyName("TotalCheck")]
        public decimal TotalCheck { get; set; }
        [JsonPropertyName("AmountOfFundsIssued")]
        public decimal AmountOfFundsIssued { get; set; }
        [JsonPropertyName("AmountOfFundsReceived")]
        public decimal AmountOfFundsReceived { get; set; }
    }
}
