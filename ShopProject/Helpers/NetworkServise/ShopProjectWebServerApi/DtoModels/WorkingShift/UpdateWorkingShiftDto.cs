using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.MediaAccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.WorkingShift
{
    public class UpdateWorkingShiftDto
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("FiscalNumberRRO")]
        public string FiscalNumberRRO { get; set; } = string.Empty;
        [JsonPropertyName("FactoryNumberRRO")]
        public string FactoryNumberRRO { get; set; } = string.Empty;
        [JsonPropertyName("DataPacketIdentifier")]
        public decimal DataPacketIdentifier { get; set; } = decimal.Zero;
        [JsonPropertyName("TypeRRO")]
        public decimal TypeRRO { get; set; } = decimal.Zero;
        [JsonPropertyName("TypeShiftCrateAt")]
        public int TypeShiftCrateAt { get; set; }
        [JsonPropertyName("TypeShiftEndAt")]
        public int TypeShiftEndAt { get; set; }
        [JsonPropertyName("TotalCheckForShift")]
        public decimal TotalCheckForShift { get; set; } = decimal.Zero;
        [JsonPropertyName("TotalReturnCheckForShift")]
        public decimal TotalReturnCheckForShift { get; set; } = decimal.Zero;
        [JsonPropertyName("AmountOfOfficialFundsReceivedCash")]
        public decimal AmountOfOfficialFundsReceivedCash { get; set; } = decimal.Zero;
        [JsonPropertyName("AmountOfOfficialFundsIssuedCash")]
        public decimal AmountOfOfficialFundsIssuedCash { get; set; } = decimal.Zero;
        [JsonPropertyName("AmountOfOfficialFundsReceivedCard")]
        public decimal AmountOfOfficialFundsReceivedCard { get; set; } = decimal.Zero;
        [JsonPropertyName("AmountOfOfficialFundsIssuedCard")]
        public decimal AmountOfOfficialFundsIssuedCard { get; set; } = decimal.Zero;
        [JsonPropertyName("AmountOfFundsReceived")]
        public decimal AmountOfFundsReceived { get; set; } = decimal.Zero;
        [JsonPropertyName("AmountOfFundsIssued")]
        public decimal AmountOfFundsIssued { get; set; } = decimal.Zero;
        [JsonPropertyName("MACCreateAt")]
        public CreateMediaAccessControlDto? MACCreateAt { get; set; }
        [JsonPropertyName("MACEndAt")]
        public CreateMediaAccessControlDto? MACEndAt { get; set; }
        [JsonPropertyName("UserOpenShiftID")]
        public string UserOpenShiftID { get; set; } = string.Empty;
        [JsonPropertyName("UserCloseShiftID")]
        public string UserCloseShiftID { get; set; } = string.Empty;
    }
}
