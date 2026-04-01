using ShopProject.Model.UI.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping
{
    internal static class UserRoleMappingExtensions
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
    }
}
