using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.DtoModels.User;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class UserMappingExtensions
    {
        public static UserEntity ToUserEntity(this CreateUserDto item)
        {
            var userEntity = new UserEntity()
            {
                SignatureKey = item.SignatureKey,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Password = item.Password,
                TIN = item.TIN,
                UserRole = new UserRoleEntity() { ID = item.UserRole_ID }
            };

            Enum.TryParse(item.Status.ToString(), out TypeStatusUser type);
            userEntity.Status = type;
            return userEntity;
        }

        public static UserEntity ToUserEntity(this UpdateUserDto item)
        {
            var userEntity = new UserEntity()
            {
                ID = item.ID,
                SignatureKey = item.SignatureKey,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Password = item.Password,
                TIN = item.TIN,
                UserRole = new UserRoleEntity() { ID = item.UserRole_ID }
            };

            Enum.TryParse(item.Status.ToString(), out TypeStatusUser type);
            userEntity.Status = type;
            return userEntity;
        }
    }
}
