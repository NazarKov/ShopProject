using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.Domain.UserRole;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.SignatureKey;
using ShopProject.Model.UI.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.User
{
    internal class UserModel
    {
        public Guid ID { get; set; }
        public string Login { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TIN { get; set; } = string.Empty;
        public bool AutomaticLogin { get; set; } 
        public TypeStatusUser Status { get; set; } 
        public UserRoleModel? Role { get; set; } 
        public SignatureKeyModel? SignatureKey { get; set; } 
        public DateTimeOffset? CreatedAt { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
