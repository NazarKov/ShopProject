using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.UserRole
{
    public static class ApiUserRoleMappingExtensions
    {
        public static ShopProject.Model.Domain.UserRole.UserRole ToUserRole(this UserRoleDto role)
        {
            return new ShopProject.Model.Domain.UserRole.UserRole()
            {
                ID = role.ID,
                NameRole = role.NameRole,
                TypeAccess = role.TypeAccess,
            };
        }
        public static IEnumerable<ShopProject.Model.Domain.UserRole.UserRole> ToUserRole(this IEnumerable<UserRoleDto> roles)
        {
            var result = new List<ShopProject.Model.Domain.UserRole.UserRole>();
            foreach (var role in roles)
            {
                result.Add(ToUserRole(role));
            }
            return result;
        }

        public static UserRoleDto ToUserRoleDto(this ShopProject.Model.Domain.UserRole.UserRole role)
        {
            return new UserRoleDto()
            {
                ID = role.ID,
                NameRole = role.NameRole, 
                TypeAccess = role.TypeAccess,
            };
        }
        public static IEnumerable<UserRoleDto> ToUserRoleDto(this IEnumerable<ShopProject.Model.Domain.UserRole.UserRole> roles)
        {
            var result = new List<UserRoleDto>();
            foreach (var role in roles)
            {
                result.Add(ToUserRoleDto(role));
            }
            return result;
        }
    }
}
