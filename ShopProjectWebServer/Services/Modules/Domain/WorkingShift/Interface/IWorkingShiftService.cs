using ShopProjectWebServer.Api.DtoModels.WorkingShift;
using ShopProjectWebServer.Models.Domain.WorkingShift;
using ShopProjectWebServer.Services.Common;
using WorkingShiftModel = ShopProjectWebServer.Models.Domain.WorkingShift.WorkingShift;

namespace ShopProjectWebServer.Services.Modules.Domain.WorkingShift.Interface
{
    public interface IWorkingShiftService
    {
        public Task<OperationResult<WorkingShiftModel>> Add(WorkingShiftModel item);
        public Task<OperationResult<WorkingShiftModel>> Update(WorkingShiftModel item); 
        public Task<OperationResult<WorkingShiftModel>> GetById(int id);
        public Task<OperationResult<WorkingShiftResourse>> GetResourseByWorkingShift(string fisclaNumberRRo);
    }
}
