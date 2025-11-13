using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class MediaAccessMappingExtensions
    {
        public static MediaAccessControlEntity ToMediaAccessEntity(this CreateMediaAccessControlDto mediaAccessControl)
        {
            var entity = new MediaAccessControlEntity();
            entity.Content = mediaAccessControl.Content; 
            if(mediaAccessControl.WorkingShiftsID != 0)
            {
                entity.WorkingShifts = new WorkingShiftEntity() { ID = mediaAccessControl.WorkingShiftsID };
            }
            if(mediaAccessControl.OperationsRecorderID != Guid.Empty)
            {
                entity.OperationsRecorder = new OperationsRecorderEntity() { ID = mediaAccessControl.OperationsRecorderID };
            }
            if(mediaAccessControl.OperationID != null)
            {
                entity.Operation = new OperationEntity() { ID = mediaAccessControl.OperationID };
            }

            return entity;
        }

        public static MediaAccessControlDto ToMediaAccessDto(this MediaAccessControlEntity mediaAccessControl)
        {
            return new MediaAccessControlDto()
            {
                Content = mediaAccessControl.Content,
                SequenceNumber = mediaAccessControl.SequenceNumber,
                
            };
        }

    }
}
