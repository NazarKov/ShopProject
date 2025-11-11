using ShopProjectDataBase.Entities;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.Token
{
    public class TokenDto
    {
        [JsonPropertyName("UserID")]
        public string? UserID { get; set; }
        [JsonPropertyName("Token")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("Device")]
        public string Device { get; set; } = string.Empty;
        [JsonPropertyName("CreateAt")]
        public DateTime CreateAt { get; set; }
    }
}
