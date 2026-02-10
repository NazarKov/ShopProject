using ShopProjectWebServer.Api.DtoModels.UserRole;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class UserRoleServise : IUserRoleServise
    {
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public UserRoleServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
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
