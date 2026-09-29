using ShopProjectWebServer.Models.Domain.Enum; 

namespace ShopProjectWebServer.Models.Domain.BootStrap
{
    public class WorkingShiftStatus
    {
        public Guid UserID { get; set; }
        public WorkingShift.WorkingShift WorkingShift { get; set; } = new Domain.WorkingShift.WorkingShift();
        public TaxObject.TaxObject TaxObject { get; set; } = new TaxObject.TaxObject();
        public TypeStatusShift Status { get; set; }
        public DateTime? OpenShiftTime { get; set; }
        public OperationRecorder.OperationRecorder OperationRecorder { get; set; } = new OperationRecorder.OperationRecorder();  
    }
}
