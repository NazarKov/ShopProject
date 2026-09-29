using ShopProject.Model.Enum; 
using ShopProject.Model.UI.SignatureKey;
using ShopProject.Model.UI.UserRole;
using System; 
using System.Linq;  

namespace ShopProject.Model.UI.User
{
    public class UserModel
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

        public string StatusString
        {
            get { return UserStatusModel.GetUserStatus().ElementAt(System.Enum.GetValues<TypeStatusUser>().ToList().IndexOf(Status)); }
            set { Status = System.Enum.GetValues<TypeStatusUser>().ToList().ElementAt(UserStatusModel.GetUserStatus().IndexOf(value)); }
        }

        public string Name
        {
            get { if (!string.IsNullOrEmpty(FullName))
                {
                    return FullName;
                }
                else 
                {
                    return Login;
                } 
            }
        }
        public string SignatureKeyIsCreate
        {
            get 
            { 
                if (SignatureKey != null && SignatureKey.Signature != null)
                {
                    return "Ключ підключено";
                }
                else
                {
                    return "Ключ відсутній";
                }
            }

        }

    }
}
