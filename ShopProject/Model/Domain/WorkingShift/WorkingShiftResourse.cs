
namespace ShopProject.Model.Domain.WorkingShift
{
    public class WorkingShiftResourse
    {         public int ID; 
        public ShopProject.Model.Domain.MediaAccessControl.MediaAccessControl MediaAccessControl { get; set; } 
        public string OperationNumber { get; set; }
    }
}
