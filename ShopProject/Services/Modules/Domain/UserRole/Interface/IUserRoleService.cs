using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleModel = ShopProject.Model.Domain.UserRole.UserRole;

namespace ShopProject.Services.Modules.Domain.UserRole.Interface
{
    internal interface IUserRoleService
    {
        public IEnumerable<UserRoleModel> GetFromSession();
    }
}
