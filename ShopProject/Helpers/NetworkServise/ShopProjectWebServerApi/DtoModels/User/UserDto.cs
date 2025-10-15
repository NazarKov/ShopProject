using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.User
{
    public class UserDto
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("tIN")]
        public string TIN { get; set; } = string.Empty;
        [JsonPropertyName("automaticLogin")]
        public bool AutomaticLogin { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("userRoleID")]
        public int? UserRoleID { get; set; }
        [JsonPropertyName("signatureKeyID")]
        public Guid? SignatureKeyID { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
