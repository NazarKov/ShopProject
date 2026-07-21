using ShopProject.Model.Enum;
using System;
using System.Text.Json.Serialization;

namespace ShopProject.Model.Domain.WorkingShift
{
    public class WorkingShiftStatus
    { 
        public WorkingShift? WorkingShift { get; set; }
        public TaxObject.TaxObject? TaxObject { get; set; }
        public TypeStatusShift Status { get; set; }
        public DateTime? OpenShiftTime { get; set; }
        public OperationRecorder.OperationRecorder? OperationRecorder { get; set; }
        [JsonIgnore]
        public MediaAccessControl.MediaAccessControl? MediaAccessControl { get; set; } 
    }
}
