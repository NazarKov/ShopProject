using ShopProjectWebServer.Models.Domain.BootStrap;
using ShopProjectWebServer.Services.Common;

namespace ShopProjectWebServer.Services.Modules.BootStrap.Interface
{
    public interface IBootStrapService
    {
        public OperationResult<StartData> GetStartData();
        public Task<OperationResult<WorkingShiftStatus>> GetWrokingShiftData(Guid userID);
    }
}
