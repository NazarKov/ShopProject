using ShopProject.Model.Domain.UserRole;
using ShopProject.Services.Modules.Domain.UserRole.Interface;
using ShopProject.Services.Modules.Session.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleModel = ShopProject.Model.Domain.UserRole.UserRole;

namespace ShopProject.Services.Modules.Domain.UserRole
{
    internal class UserRoleService : IUserRoleService
    {
        private readonly ISessionService _sessionService;

        public UserRoleService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }


        public IEnumerable<UserRoleModel> GetFromSession()
        {
            return _sessionService.Roles;
        }
    }
}
