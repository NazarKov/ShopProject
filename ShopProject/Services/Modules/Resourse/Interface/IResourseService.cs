using ShopProject.Model.Domain.User;
using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Services.Modules.Common; 
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Resourse.Interface
{
    internal interface IResourseService
    {
        public bool IsInitSystemFolders();
        public Task LoadStartData(); 
        public Task<OperationResult<WorkingShiftStatus>> LoadWorkingShift();
    }
}
