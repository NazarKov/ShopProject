using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;
using ShopProjectWebServer.Models.Domain.MediaAccessControl;
using MediaAccessControlModel = ShopProjectWebServer.Models.Domain.MediaAccessControl.MediaAccessControl;

namespace ShopProjectWebServer.Services.Modules.Mapping.MediaAccessControl
{
    public static class MediaAccessControlApiMappingExtensions
    {
        public static MediaAccessControlModel ToMediaAccessControl(this CreateMediaAccessControlDto item)
        {
            var result = new MediaAccessControlModel()
            {
                ID = item.ID,
                Content = item.Content,
                Operation = new Models.Domain.Operation.Operation() { ID = item.OperationID }, 
                OperationsRecorder = new Models.Domain.OperationRecorder.OperationRecorder() { ID = item.OperationsRecorderID }
            };
            if(item.WorkingShiftsID == 0)
            {
                result.WorkingShifts = null;
            }
            else
            {
                result.WorkingShifts = new Models.Domain.WorkingShift.WorkingShift() { ID = item.WorkingShiftsID };
            }
            return result;
        }

        public static MediaAccessControlDto ToMediaAccessControlDto(this MediaAccessControlModel item)
        {
            return new MediaAccessControlDto()
            {
                SequenceNumber = item.SequenceNumber,
                Content = item.Content,
            };
        }
    }
}
