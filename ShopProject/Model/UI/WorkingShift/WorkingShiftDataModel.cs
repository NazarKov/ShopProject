using ShopProject.Model.Enum;
using ShopProject.Model.UI.MediaAccessControl;
using ShopProject.Model.UI.OperationRecorder; 
using ShopProject.Model.UI.TaxObject;
using System;
using System.Linq;
using System.Text.Json.Serialization; 

namespace ShopProject.Model.UI.WorkingShift
{
    internal class WorkingShiftDataModel
    { 
        public WorkingShiftModel? WorkingShift { get; set; }
        public TaxObjectModel? TaxObject { get; set; }
        public TypeStatusShift Status { get; set; }
        public DateTime? OpenShiftTime { get; set; }
        public bool IsTestMode { get; set; } = true;
        public OperationRecorderModel? OperationRecorder { get; set; } 
        public MediaAccessControlModel? MediaAccessControl { get; set; }

        public WorkingShiftDataModel() { }
        public WorkingShiftDataModel(WorkingShiftDataModel item)
        {
            WorkingShift = item.WorkingShift;
            TaxObject = item.TaxObject;
            Status = item.Status;
            OpenShiftTime = item.OpenShiftTime;
            IsTestMode = item.IsTestMode;
            OperationRecorder = item.OperationRecorder;
            MediaAccessControl = item.MediaAccessControl;
        }
        public string StatusString
        {
            get { return WorkingShiftStatusModel.GetWorkingShiftStatus().ElementAt(System.Enum.GetValues<TypeStatusShift>().ToList().IndexOf(Status)); }
            set { Status = System.Enum.GetValues<TypeStatusShift>().ToList().ElementAt(WorkingShiftStatusModel.GetWorkingShiftStatus().IndexOf(value)); }
        }
    }
}
