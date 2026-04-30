using ShopProjectDataBase.Entities;

namespace ShopProjectWebServer.Services.Modules.Mapping
{
    public static class UserMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.User.User ToUser(this UserEntity item)
        {
            var result = new Models.Domain.User.User()
            {
                ID = item.ID,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Status = item.Status,
                TIN = item.TIN,
                Password = item.Password,
            };
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
        public static IEnumerable<ShopProjectWebServer.Models.Domain.User.User> ToUser(this IEnumerable<UserEntity> items)
        {
            var result = new List<ShopProjectWebServer.Models.Domain.User.User>();
            foreach(var item in items)
            {
                result.Add(ToUser(item));
            }
            return result;
        }

        public static UserEntity ToUser(this ShopProjectWebServer.Models.Domain.User.User item)
        {
            var result = new UserEntity()
            {
                ID = item.ID,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Status = item.Status,
                TIN = item.TIN,
                Password = item.Password
            };
            if (item.UserRole != null)
            {
                result.UserRole = item.UserRole.ToUserRoleEntity();
            } 
            return result;
        }
    }
}
