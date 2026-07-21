using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Model.UI.WorkingShift;
using ShopProject.Services.Modules.Mapping.OperationRecorder;
using ShopProject.Services.Modules.Mapping.TaxObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.WorkingShift
{
    internal static class UiWorkingShiftMappingExtensions
    {
        public static WorkingShiftDataModel ToWorkingShiftData(this WorkingShiftStatus item)
        {
            var result = new WorkingShiftDataModel();
            if (item.OperationRecorder != null) 
            {
                result.OperationRecorder = item.OperationRecorder.ToOperationRecorderModel();
            }
            if (item.TaxObject != null)
            {
                result.TaxObject = item.TaxObject.ToTaxObjectModel();
            }
            result.Status = item.Status;
            result.OpenShiftTime = item.OpenShiftTime; 
            return result;
        }
    }
}
