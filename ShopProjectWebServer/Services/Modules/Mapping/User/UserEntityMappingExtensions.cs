using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Models.Domain.Enum;
using ShopProjectWebServer.Services.Modules.Mapping.SignatureKey;
using ShopProjectWebServer.Services.Modules.Mapping.UserRole;

namespace ShopProjectWebServer.Services.Modules.Mapping.User
{
    public static class UserEntityMappingExtensions
    {
        public static Models.Domain.User.User ToUser(this UserEntity item)
        {
            var result = new Models.Domain.User.User()
            {
                ID = item.ID,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Status = (Models.Domain.Enum.TypeStatusUser)item.Status,
                TIN = item.TIN,
                Password = item.Password,
            };
            if(item.SignatureKey != null)
            {
                result.SignatureKey = item.SignatureKey.ToSignatureKey();
            }
            if (item.UserRole != null)
            {
                result.UserRole = item.UserRole.ToUserRole();
            }
            if (item.Tokens != null)
            {
                result.Token = item.Tokens.Reverse().First().Token;
            }
            return result;
        }
        public static IEnumerable<Models.Domain.User.User> ToUser(this IEnumerable<UserEntity> items)
        {
            var result = new List<Models.Domain.User.User>();
            foreach(var item in items)
            {
                result.Add(ToUser(item));
            }
            return result;
        }

        public static UserEntity ToUserEntity(this Models.Domain.User.User item)
        {
            var result = new UserEntity()
            {
                ID = item.ID,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Status = (ShopProjectDataBase.Helper.TypeStatusUser)item.Status,
                TIN = item.TIN,
                Password = item.Password
            };
            if (item.UserRole != null)
            {
                result.UserRole = item.UserRole.ToUserRoleEntity();
            }
            if (item.SignatureKey != null) 
            {
                result.SignatureKey = new ElectronicSignatureKey();
                result.SignatureKey.Signature = item.SignatureKey.Signature;
                result.SignatureKey.SignaturePassword = item.SignatureKey.SignaturePassword;
                result.SignatureKey.CreateAt = item.SignatureKey.CreateAt;
                result.SignatureKey.EndAt = item.SignatureKey.EndAt;
            }

            return result;
        }
        public static IEnumerable<UserEntity> ToUserEntity(this IEnumerable<ShopProjectWebServer.Models.Domain.User.User> items) 
        {
            var result = new List<UserEntity>();
            foreach(var item in items)
            {
                result.Add(ToUserEntity(item));
            }
            return result;
        }
    }
}
