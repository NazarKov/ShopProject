using ShopProjectWebServer.Api.DtoModels.UserRole;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class UserRoleServise : IUserRoleServise
    {
        public IEnumerable<UserRoleDto> GetAll(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var result = DataBaseMainController.DataBaseAccess.UserRoleTable.GetAll();

            return result.ToUserRoleDto();
        }
    }
}
