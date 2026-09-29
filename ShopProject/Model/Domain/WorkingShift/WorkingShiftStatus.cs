using ShopProject.Model.Enum;
using System;
using System.Text.Json.Serialization;

namespace ShopProject.Model.Domain.WorkingShift
{
    public class WorkingShiftStatus
    {
        public Guid UserID {  get; set; }
        public WorkingShift? WorkingShift { get; set; }
        public TaxObject.TaxObject TaxObject { get; set; } = new TaxObject.TaxObject();
        public TypeStatusShift Status { get; set; }
        public DateTime? OpenShiftTime { get; set; }
        public OperationRecorder.OperationRecorder OperationRecorder { get; set; } = new OperationRecorder.OperationRecorder();
        [JsonIgnore]
        public MediaAccessControl.MediaAccessControl? MediaAccessControl { get; set; } 
    }
}
