using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.OperationRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.ModelService.User.Interface
{
    internal interface IUserServise
    {
        public Task<Paginator<ShopProject.Model.Domain.User.User>> GetUsersPageColumn(int page, int countColumn, TypeStatusUser status);
        public Task<Paginator<ShopProject.Model.Domain.User.User>> SearchByName(string item, int page, int countColumn, TypeStatusUser status);
        public Task<bool> DeleteUser(ShopProject.Model.Domain.User.User user);
        public Task<List<OperationRecorderDialogWindowModel>> GetAllObject();
        public Task<bool> SaveBinding(ShopProject.Model.Domain.User.User user, List<OperationRecorderDialogWindowModel> objectOwnerHelpers);

        public Task<bool> LogIn(string login, string password);
        public ShopProject.Model.Domain.User.User GetUserFromSession();
    }
}
