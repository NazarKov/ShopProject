using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.TaxObjectUser
{
    internal class TaxObjectUser
    {
        public int ID { get; set; }
        public ShopProject.Model.Domain.User.User? User { get; set; }
        public ShopProject.Model.Domain.TaxObject.TaxObject? TaxObject { get; set; }
        public IEnumerable<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>? OperationRecorders { get; set; }
    }
}
