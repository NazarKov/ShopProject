using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.User
{
    public class AuthorizationUserDto
    {
        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("tIN")]
        public string TIN { get; set; } = string.Empty;
        [JsonPropertyName("automaticLogin")]
        public bool AutomaticLogin { get; set; }
        [JsonPropertyName("userRoleID")]
        public int? UserRoleID { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty; 
    }
}
