using ShopProjectDataBase.Entities;

namespace ShopProjectWebServer.Services.Modules.Mapping
{
    public static class UserRoleMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.UserRole.UserRole ToUserRole (this UserRoleEntity item)
        {
            return new Models.Domain.UserRole.UserRole()
            {
                ID = item.ID,
                NameRole = item.NameRole,
                TypeAccess = item.TypeAccess,
            };
        }

        public static UserRoleEntity ToUserRoleEntity(this ShopProjectWebServer.Models.Domain.UserRole.UserRole item)
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
