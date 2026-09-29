
using ShopProjectWebServer.Api.DtoModels.BootStrap;
using ShopProjectWebServer.Models.Domain.BootStrap;
using ShopProjectWebServer.Services.Modules.Mapping.OperationRecorder;
using ShopProjectWebServer.Services.Modules.Mapping.TaxObject;
using ShopProjectWebServer.Services.Modules.Mapping.WorkingShift;

namespace ShopProjectWebServer.Services.Modules.Mapping.BootStrap
{
    public static class WorkingShfitApiMappingExtensions
    {
        public static WorkingShiftStatusDto ToWorkingShiftStatusDto(this WorkingShiftStatus item)
        {
            return new WorkingShiftStatusDto()
            {
                Status = (int)item.Status,
                OpenShiftTime = item.OpenShiftTime,
                WorkingShift = item.WorkingShift.ToWorkingShiftDto(),
                OperationRecorder = item.OperationRecorder.ToOpeartionRecorderDto(),
                TaxObject = item.TaxObject.ToTaxObjectDto(),
                UserID = item.UserID,
            };
        }
    }
}
