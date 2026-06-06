using ShopProjectWebServer.Api.DtoModels.UserRole;
using ShopProjectWebServer.Services.Common;
using System.Runtime.CompilerServices;

namespace ShopProjectWebServer.Services.Modules.Domain.UserRole.Interface
{
    public interface IUserRoleServiсe
    {
        public OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.UserRole.UserRole>> GetAll();
    }
}
