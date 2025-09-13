using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.MediaAccessControl;
using ShopProject.UIModel.SalePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class MediaAccessMappingExtensions
    {
        public static CreateMediaAccessControlDto ToCreatMediaAccessControlDto(this UIMediaAccessControlModel mediaAccessControl)
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

        public static UIMediaAccessControlModel ToUIMediaAccessControl(this MediaAccessControlDto mediaAccessControl) 
        {
            return new UIMediaAccessControlModel()
            {
                Content = mediaAccessControl.Content,
                SequenceNumber = mediaAccessControl.SequenceNumber,
            };
        }
    }
}
