using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.SignatureKey
{
    public class SignatureKeyDto
    {
        [JsonPropertyName("Signature")]
        public byte[]? Signature { get; set; }
        [JsonPropertyName("SignaturePassword")]
        public string? SignaturePassword { get; set; }
        [JsonPropertyName("CreateAt")]
        public DateTimeOffset CreateAt { get; set; }
        [JsonPropertyName("EndAt")]
        public DateTimeOffset EndAt { get; set; }
    }
}
