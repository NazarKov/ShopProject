using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.DtoModels.SignatureKey;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.User
{
    public class CreateUserDto
    {
        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("tin")]
        public string TIN { get; set; } = string.Empty;
        [JsonPropertyName("automaticLogin")]
        public bool AutomaticLogin { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("userRoleID")]
        public int UserRoleID { get; set; }
        [JsonPropertyName("signatureKey")]
        public SignatureKeyDto? SignatureKey { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
