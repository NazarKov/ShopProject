using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.User
{
    public class UpdateUserDto
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("Login")]
        public string Login { get; set; } = string.Empty;
        [JsonPropertyName("Password")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("FullName")]
        public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("Email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("TIN")]
        public string TIN { get; set; } = string.Empty;
        [JsonPropertyName("AutomaticLogin")]
        public bool AutomaticLogin { get; set; }
        [JsonPropertyName("Status")]
        public int Status { get; set; }
        [JsonPropertyName("UserRoleID")]
        public int? UserRoleID { get; set; }
        [JsonPropertyName("SignatureKeyID")]
        public string? SignatureKeyID { get; set; }
        [JsonPropertyName("CreatedAt")]
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
