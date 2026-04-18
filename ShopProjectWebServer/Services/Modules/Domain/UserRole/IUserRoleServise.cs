using ShopProjectWebServer.Api.DtoModels.UserRole;
using System.Runtime.CompilerServices;

namespace ShopProjectWebServer.Services.Modules.Domain.UserRole
{
    public interface IUserRoleServise
    {
        public IEnumerable<UserRoleDto> GetAll(string token);
    }
}
