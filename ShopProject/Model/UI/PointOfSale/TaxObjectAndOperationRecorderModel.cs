using ShopProject.Core.Mvvm;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Model.UI.TaxObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.PointOfSale
{
    internal class TaxObjectAndOperationRecorderModel
    {
        public TaxObjectModel TaxObject { get; set; } = new TaxObjectModel();
        public List<OperationRecorderModel> OperationRecorders { get; set; } = new List<OperationRecorderModel>(); 
    }
}
