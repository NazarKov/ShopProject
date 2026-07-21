using ShopProjectDataBase.Entities; 
using MediaAccessControlModel = ShopProjectWebServer.Models.Domain.MediaAccessControl.MediaAccessControl;

namespace ShopProjectWebServer.Services.Modules.Mapping.MediaAccessControl
{
    public static class MediaAccessControlEntityMappingExtensions
    {
        public static MediaAccessControlModel ToMediaAccessControl(this MediaAccessControlEntity item)
        {
            var result = new MediaAccessControlModel()
            {
                ID = item.ID,
                Content = item.Content,
                SequenceNumber = item.SequenceNumber,
            };
            return result;
        }

        public static MediaAccessControlEntity ToMediaAccessControlEntity(this MediaAccessControlModel item)
        {
            var result = new MediaAccessControlEntity()
            {
                ID = item.ID,
                Content = item.Content,
                SequenceNumber = item.SequenceNumber,
            };
            if (item.Operation != null)
            {
                result.Operation = new OperationEntity() { ID = item.Operation.ID };
            }
            if(item.OperationsRecorder != null)
            {
                result.OperationsRecorder = new OperationsRecorderEntity() { ID = item.OperationsRecorder.ID };
            }
            if (item.WorkingShifts != null)
            {
                result.WorkingShifts = new WorkingShiftEntity() { ID = item.WorkingShifts.ID };
            } 
            return result;
        }
    }
}
