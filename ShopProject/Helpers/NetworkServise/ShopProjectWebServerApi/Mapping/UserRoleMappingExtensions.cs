using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.User;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.UserRole;
using ShopProject.UIModel.UserPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class UserRoleMappingExtensions
    {
        public static UserRole ToUserRole(this UserRoleDto role)
        {
            return new UserRole()
            {
                ID = role.ID,
                NameRole = role.NameRole,
                TypeAccess = role.TypeAccess,
            };
        }
        public static IEnumerable<UserRole> ToUserRole(this IEnumerable<UserRoleDto> roles)
        {
            var result = new List<UserRole>();
            foreach (var role in roles) 
            {
                result.Add(ToUserRole(role));
            }
            return result;
        }
    }
}
