using ShopProjectWebServer.Api.DtoModels.UserRole;

namespace ShopProjectWebServer.Services.Modules.Mapping.UserRole
{
    public static class UserRoleApiMappingExtensions
    {
        public static UserRoleDto ToUserRoleDto(this ShopProjectWebServer.Models.Domain.UserRole.UserRole item)
        {
            return new UserRoleDto()
            {
                ID = item.ID,
                NameRole = item.NameRole,
                TypeAccess = item.TypeAccess,
            };
        }
        public static IEnumerable<UserRoleDto> ToUserRoleDto(this IEnumerable<ShopProjectWebServer.Models.Domain.UserRole.UserRole> items)
        {
            var reuslt = new List<UserRoleDto>();
            foreach (var item in items)
            {
                reuslt.Add(ToUserRoleDto(item));
            }
            return reuslt;
        }
    }
}
