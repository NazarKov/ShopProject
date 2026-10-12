using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Model.UI.TaxObject; 
using System.Collections.Generic; 

namespace ShopProject.Model.UI.PointOfSale
{
    internal class TaxObjectAndOperationRecorderModel
    {
        public TaxObjectModel TaxObject { get; set; } = new TaxObjectModel();
        public List<OperationRecorderModel> OperationRecorders { get; set; } = new List<OperationRecorderModel>(); 
    }
}
