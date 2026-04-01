using ShopProject.Model.UI.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping
{
    internal static class UserMappingExtensions
    {
        public static ShopProject.Model.Domain.User.User ToUser(this UserModel item)
        {
            var result = new ShopProject.Model.Domain.User.User()
            {
                Status = item.Status,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                ID = item.ID,
                Password = item.Password,
                TIN = item.TIN,
                Token = item.Token,
            };
            if (item.Role!=null)
            {
                result.Role = item.Role.ToUserRole();
            }
            if (item.SignatureKey != null)
            {
                result.SignatureKey = item.SignatureKey.ToSignatureKey();
            }
            return result;
        }
        public static UserModel ToUserModel(this ShopProject.Model.Domain.User.User item)
        {
            var result = new UserModel()
            {
                Status = item.Status,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                ID = item.ID,
                Password = item.Password,
                TIN = item.TIN,
                Token = item.Token,
            };
            if (item.Role != null)
            {
                result.Role = item.Role.ToUserRoleModel();
            }
            if (item.SignatureKey != null)
            {
                result.SignatureKey = item.SignatureKey.ToSignatureKeyModel();
            }
            return result;
        }
    }
}
