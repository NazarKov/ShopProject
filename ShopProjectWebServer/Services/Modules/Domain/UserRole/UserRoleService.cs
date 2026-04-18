using ShopProjectWebServer.Api.DtoModels.UserRole;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Modules.Authorization;

namespace ShopProjectWebServer.Services.Modules.Domain.UserRole
{
    internal class UserRoleService : IUserRoleServise
    {
        private DataBaseService _controller;
        private AuthorizationService _authorizationServise;

        public UserRoleService(DataBaseService controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationService(controller);
        }
        public IEnumerable<UserRoleDto> GetAll(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var result = _controller.DataBaseAccess.UserRoleTable.GetAll();

            return result.ToUserRoleDto();
        }
    }
}
