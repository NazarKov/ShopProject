using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.Operation
{
    public class OperaiontStatisticsDto
    {
        [JsonPropertyName("TotalCheck")]
        public decimal TotalCheck { get; set; }
        [JsonPropertyName("AmountOfFundsIssued")]
        public decimal AmountOfFundsIssued { get; set; }
        [JsonPropertyName("AmountOfFundsReceived")]
        public decimal AmountOfFundsReceived { get; set; }
        [JsonPropertyName("AmountOfOfficialFundsIssued")]
        public decimal AmountOfOfficialFundsIssued { get; set; }
        [JsonPropertyName("AmountOfOfficialFundsReceived")]
        public decimal AmountOfOfficialFundsReceived { get; set; }
        [JsonPropertyName("TotalReturnCheck")]
        public decimal TotalReturnCheck { get; set; }

    }
}
