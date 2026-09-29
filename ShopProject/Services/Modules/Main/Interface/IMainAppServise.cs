using ShopProject.Model.Domain.User;
using ShopProject.Services.Modules.Common; 
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Main.Interface
{
    internal interface IMainAppServise
    {
        public Task<bool> IsConnectServer();
        public Task LoadStartData();
        public Task<OperationResult<User>> LoadUserData();
        public Task LoadSession(); 
    }
}
