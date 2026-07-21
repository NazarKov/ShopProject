
namespace ShopProjectWebServer.Models.Domain.MediaAccessControl
{
    public class MediaAccessControl
    { 
        public int ID { get; set; } 
        public string Content { get; set; } = string.Empty; 
        public int SequenceNumber { get; set; } 
        public ShopProjectWebServer.Models.Domain.WorkingShift.WorkingShift? WorkingShifts { get; set; } 
        public ShopProjectWebServer.Models.Domain.Operation.Operation? Operation { get; set; } 
        public ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder? OperationsRecorder { get; set; }
    }
}
