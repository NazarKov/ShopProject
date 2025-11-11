using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.User
{
    public class AuthorizationUserDto
    {
        [JsonPropertyName("Login")]
        public string Login { get; set; } = string.Empty;
        [JsonPropertyName("FullName")]
        public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("Email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("TIN")]
        public string TIN { get; set; } = string.Empty;
        [JsonPropertyName("AutomaticLogin")]
        public bool AutomaticLogin { get; set; }
        [JsonPropertyName("UserRoleID")]
        public int? UserRoleID { get; set; }
        [JsonPropertyName("Token")]
        public string Token { get; set; } = string.Empty;
    }
}
