using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.OperationRecorder;
using ShopProject.Model.Domain.WorkingShift; 

namespace ShopProject.Model.Domain.MediaAccessControl
{
    public class MediaAccessControl
    {
        public int ID { get; set; } 
        public string Content { get; set; } = string.Empty; 
        public int SequenceNumber { get; set; } 
        public WorkingShift.WorkingShift? WorkingShifts { get; set; } 
        public Operation.Operation? Operation { get; set; } 
        public OperationRecorder.OperationRecorder? OperationsRecorder { get; set; }
    }
}
