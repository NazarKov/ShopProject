using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Token
{
    public class TokenDto
    {
        [JsonPropertyName("userID")]
        public string? UserID { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("device")]
        public string Device { get; set; } = string.Empty;
        [JsonPropertyName("createAt")]
        public DateTime CreateAt { get; set; }
    }
}
