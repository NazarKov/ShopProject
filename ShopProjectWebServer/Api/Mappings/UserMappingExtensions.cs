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
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Password = item.Password,
                TIN = item.TIN,
                UserRole = new UserRoleEntity() { ID = item.UserRoleID }
            };

            if (item.SignatureKey != null) 
            {
                userEntity.SignatureKey = new ElectronicSignatureKey();
                userEntity.SignatureKey.CreateAt = item.SignatureKey.CreateAt;
                userEntity.SignatureKey.EndAt = item.SignatureKey.EndAt;
                userEntity.SignatureKey.SignaturePassword = item.SignatureKey.SignaturePassword;
                userEntity.SignatureKey.Signature = item.SignatureKey.Signature;
            }
            

            Enum.TryParse(item.Status.ToString(), out TypeStatusUser type);
            userEntity.Status = type;
            return userEntity;
        }

        public static UserEntity ToUserEntity(this UpdateUserDto item)
        {
            var userEntity = new UserEntity()
            {
                ID = Guid.Parse(item.ID),
                SignatureKey = new ElectronicSignatureKey() { ID = Guid.Parse(item.SignatureKeyID)},
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Password = item.Password,
                TIN = item.TIN,
                UserRole = new UserRoleEntity() { ID = item.UserRoleID.Value }
            };

            Enum.TryParse(item.Status.ToString(), out TypeStatusUser type);
            userEntity.Status = type;
            return userEntity;
        }
        public static UserDto ToUserDto(this UserEntity item) 
        { 
            var result = new UserDto()
            {
                ID = item.ID.ToString(),
                UserRoleID = item.UserRole.ID, 
                Status = (int)item.Status,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Password = item.Password,
                TIN = item.TIN,
            }; 
            if (item.SignatureKey != null) {

                result.SignatureKeyID = item.SignatureKey.ID.ToString();
            }
            return result; 
        }
        public static IEnumerable<UserDto> ToUserDto(this IEnumerable<UserEntity> item) 
        {
            var result = new List<UserDto>();
            foreach (var itemEntity in item) 
            {
                result.Add(ToUserDto(itemEntity));
            }
            return result;
        }
    }
}
