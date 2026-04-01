using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.MediaAccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping
{
    public static class MediaAccessMappingExtensions
    {
        public static CreateMediaAccessControlDto ToCreatMediaAccessControlDto(this MediaAccessControl mediaAccessControl)
        {
            var item = new CreateMediaAccessControlDto()
            {
                Content = mediaAccessControl.Content 
            };

            if(mediaAccessControl.Operation !=null)
            {
                item.OperationID = mediaAccessControl.Operation.ID;
            }
            if (mediaAccessControl.WorkingShifts != null) 
            {
                item.WorkingShiftsID = mediaAccessControl.WorkingShifts.ID;
            }
            if (mediaAccessControl.OperationsRecorder != null) 
            {
                item.OperationsRecorderID = mediaAccessControl.OperationsRecorder.ID;
            }


            return item;
        }

        public static MediaAccessControl ToMediaAccessControl(this MediaAccessControlDto mediaAccessControl) 
        {
            return new MediaAccessControl()
            {
                Content = mediaAccessControl.Content,
                SequenceNumber = mediaAccessControl.SequenceNumber,
            };
        }
    }
}
