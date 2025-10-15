using ShopProjectDataBase.Entities;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.SignatureKey
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
