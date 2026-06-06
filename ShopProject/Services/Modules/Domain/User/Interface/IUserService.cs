using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Services.Modules.Common;
using System.Collections.Generic; 
using System.Threading.Tasks;
using UserModel = ShopProject.Model.Domain.User.User;

namespace ShopProject.Services.Modules.Domain.User.Interface
{
    internal interface IUserService
    {
        public Task<OperationResult<Paginator<UserModel, TypeStatusUser>>> GetPageColumn(int page, int countColumn, TypeStatusUser status);
        public Task<OperationResult<Paginator<UserModel, TypeStatusUser>>> SearchByName(string item, int page, int countColumn, TypeStatusUser status); 
        public Task<OperationResult<UserModel>> CreateUser(string login, string email, string name, string password, string pathKey, string passwordKey, ShopProject.Model.Domain.UserRole.UserRole role);
        public Task<OperationResult<UserModel>> UpdateUser(UserModel user, string pathKey, string passwordKey);

        public Task<bool> DeleteUser(ShopProject.Model.Domain.User.User user);
        public Task<List<OperationRecorderDialogWindowModel>> GetAllObject();
        public Task<bool> SaveBinding(ShopProject.Model.Domain.User.User user, List<OperationRecorderDialogWindowModel> objectOwnerHelpers);

        public Task<bool> LogIn(string login, string password);
        public void SetUpdateUserInSession(UserModel user);
        public UserModel GetUpdateUserFromSession();
        public UserModel GetUserFromSession();
    }
}
