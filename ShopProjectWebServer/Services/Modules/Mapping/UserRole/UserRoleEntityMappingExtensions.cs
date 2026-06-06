using ShopProjectDataBase.Entities;

namespace ShopProjectWebServer.Services.Modules.Mapping.UserRole
{
    public static class UserRoleEntityMappingExtensions
    {
        public static Models.Domain.UserRole.UserRole ToUserRole (this UserRoleEntity item)
        {
            return new Models.Domain.UserRole.UserRole()
            {
                ID = item.ID,
                NameRole = item.NameRole,
                TypeAccess = item.TypeAccess,
            };
        }

        public static IEnumerable<Models.Domain.UserRole.UserRole> ToUserRole(this IEnumerable<UserRoleEntity> items)
        {
            var result = new List<Models.Domain.UserRole.UserRole>();
            foreach (var item in items)
            {
                result.Add(ToUserRole(item));
            };
            return result;
        }

        public static UserRoleEntity ToUserRoleEntity(this Models.Domain.UserRole.UserRole item)
        {
            return new UserRoleEntity()
            {
                ID = item.ID,
                NameRole = item.NameRole,
                TypeAccess = item.TypeAccess,
            };
        }
    }
}
