using ShopProject.UIModel.ObjectOwnerPage; 
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.OperationRecorderPage
{
    public class OperationRecorder
    {
        public Guid ID { get; set; }
        public string FiscalNumber { get; set; } = string.Empty;
        public string LocalNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public TypeStatusOperationRecorder TypeStatus { get; set; }
        public DateTimeOffset D_REG { get; set; }
        public string Address { get; set; } = string.Empty;
        public ObjectOwner? ObjectOwner { get; set; }
    }
}
