using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Token
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
