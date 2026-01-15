using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Operation
{
    public class OperationInfoDto
    {
        [JsonPropertyName("TotalCheck")]
        public decimal TotalCheck { get; set; }
        [JsonPropertyName("AmountOfFundsIssued")]
        public decimal AmountOfFundsIssued { get; set; }
        [JsonPropertyName("AmountOfFundsReceived")]
        public decimal AmountOfFundsReceived { get; set; }
    }
}
