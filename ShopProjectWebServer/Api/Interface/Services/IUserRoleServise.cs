using ShopProjectWebServer.Api.DtoModels.UserRole;
using System.Runtime.CompilerServices;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IUserRoleServise
    {
        public IEnumerable<UserRoleDto> GetAll(string token);
    }
}
