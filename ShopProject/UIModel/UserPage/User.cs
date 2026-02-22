using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.UIModel.UserPage
{
    public class User
    {
        [JsonIgnore]
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
        public UserRole? Role { get; set; }
        [JsonIgnore]
        public SignatureKey? SignatureKey { get; set; }
        [JsonIgnore]
        public DateTimeOffset? CreatedAt { get; set; }
        public string? Token { get; set; }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
        public static User? Deserialize(string jason)
        {
            if (jason != null && jason != string.Empty)
            {
                return JsonSerializer.Deserialize<User>(jason);
            }
            else
            {
                return null;
            }
        }
    }
}
