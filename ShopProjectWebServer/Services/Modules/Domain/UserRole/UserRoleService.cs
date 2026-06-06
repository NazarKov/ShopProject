using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Common.Enum; 
using ShopProjectWebServer.Services.Modules.Domain.UserRole.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.UserRole;

namespace ShopProjectWebServer.Services.Modules.Domain.UserRole
{
    internal class UserRoleService : IUserRoleServiсe
    {
        private DataBaseService _controller; 

        public UserRoleService(DataBaseService controller)
        {
            _controller = controller; 
        }
        public OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.UserRole.UserRole>> GetAll()
        {
            try
            {
                var result = _controller.DataBaseAccess.UserRoleTable.GetAll();
                return OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.UserRole.UserRole>>.Success(result.ToUserRole());
            }
            catch (Exception ex) 
            {
                return OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.UserRole.UserRole>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }   
        }
    }
}
