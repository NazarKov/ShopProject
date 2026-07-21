
namespace ShopProjectWebServer.Models.Domain.WorkingShift
{
    public class WorkingShiftResourse
    {
        public int ID;
        public ShopProjectWebServer.Models.Domain.MediaAccessControl.MediaAccessControl MediaAccessControl { get; set; }
        public string OperationNumber { get; set; }
    }
}
