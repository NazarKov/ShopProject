using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.BootStrap;
using ShopProject.Services.Modules.Mapping.OperationRecorder;
using ShopProject.Services.Modules.Mapping.TaxObject;
using ShopProject.Services.Modules.Mapping.WorkingShift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.BootStrap
{
    public static class ApiWorkingShiftStatusMappingExtensions
    {
        public static WorkingShiftStatus ToWorkingShiftStatusDto(this WorkingShiftStatusDto item)
        {
            return new WorkingShiftStatus()
            {
                Status = (TypeStatusShift)item.Status,
                OpenShiftTime = item.OpenShiftTime,
                WorkingShift = item.WorkingShift.ToWorkingShift(),
                OperationRecorder = item.OperationRecorder.ToOperationRecorder(),
                TaxObject = item.TaxObject.ToTaxObject(),
                UserID = item.UserID,
            };
        }
    }
}
