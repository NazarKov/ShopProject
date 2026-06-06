using ShopProjectWebServer.Api.DtoModels.User;
using ShopProjectWebServer.Services.Common;

namespace ShopProjectWebServer.Services.Modules.Domain.User.Interface
{
    public interface IUserService
    { 
        public Task<OperationResult<ShopProjectWebServer.Models.Domain.User.User>> Add(ShopProjectWebServer.Models.Domain.User.User user);
        public Task<OperationResult<bool>> Update(ShopProjectWebServer.Models.Domain.User.User user);
        public Task<OperationResult<bool>> UpdateParameter(string id, string nameParameter, object value);
        public OperationResult<bool> Delete(string id);
       
        public OperationResult<ShopProjectWebServer.Models.Domain.User.User> Authorization(string login, string password, string devise);

        public OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.User.User>> GetUsers();
        public OperationResult<ShopProjectWebServer.Models.Domain.User.User> GetById(string id);
        public OperationResult<ShopProjectWebServer.Models.Domain.User.User> GetUser(string token);

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.User.User, int>> GetByNamePageColumn(string name,
            ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.User.User, int> paginator);
        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.User.User, int>> GetPageColumn(
            ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.User.User, int> paginator);
    }
}
