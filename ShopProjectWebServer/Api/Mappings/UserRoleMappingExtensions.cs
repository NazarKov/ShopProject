using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.UserRole;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class UserRoleMappingExtensions
    {
        public static UserRoleDto ToUserRoleDto(this UserRoleEntity item)
        {
<<<<<<< HEAD
            return new UserRoleDto() { NameRole = item.NameRole, TypeAccess = item.TypeAccess , ID = item.ID };
=======
            return new UserRoleDto() { NameRole = item.NameRole, TypeAccess = item.TypeAccess };
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        }
        public static IEnumerable<UserRoleDto> ToUserRoleDto(this IEnumerable<UserRoleEntity> items) 
        {
            var result = new List<UserRoleDto>();
            foreach (var item in items)
            {
                result.Add(ToUserRoleDto(item));
            }
            return result;
        } 
    }
}
