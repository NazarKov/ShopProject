using ShopProject.Model.UI.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.UserRole
{
    internal static class UiUserRoleMappingExtensions
    {
        public static ShopProject.Model.Domain.UserRole.UserRole ToUserRole(this UserRoleModel item)
        {
            return new ShopProject.Model.Domain.UserRole.UserRole()
            {
                ID = item.ID,
                NameRole = item.NameRole,
                TypeAccess = item.TypeAccess,
            };
        }
        public static UserRoleModel ToUserRoleModel(this ShopProject.Model.Domain.UserRole.UserRole item)
        {
            return new UserRoleModel()
            {
                ID = item.ID,
                NameRole = item.NameRole,
                TypeAccess = item.TypeAccess,
            };
        }
        public static IEnumerable<UserRoleModel> ToUserRoleModel(this IEnumerable<ShopProject.Model.Domain.UserRole.UserRole> items)
        {
            var result = new List<UserRoleModel>();
            foreach (var item in items) 
            {
                result.Add(ToUserRoleModel(item));
            }
            return result;
        }
    }
}
