using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.Domain.UserRole;
using ShopProject.Model.Enum;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProject.Model.Domain.User
{
    public class User
    { 
        public Guid ID { get; set; }
        public string Login { get; set; } = string.Empty;
        [JsonIgnore]
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TIN { get; set; } = string.Empty;
        public bool AutomaticLogin { get; set; }
        [JsonIgnore]
        public TypeStatusUser Status { get; set; }
        [JsonIgnore]
        public UserRole.UserRole? Role { get; set; }
        [JsonIgnore]
        public SignatureKey.SignatureKey? SignatureKey { get; set; }
        [JsonIgnore]
        public DateTimeOffset? CreatedAt { get; set; }
        public string Token { get; set; } = string.Empty; 
    }
}
