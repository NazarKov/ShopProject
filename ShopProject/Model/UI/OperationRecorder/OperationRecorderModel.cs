using ShopProject.Model.Enum;
using ShopProject.Model.UI.TaxObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.OperationRecorder
{
    internal class OperationRecorderModel
    {
        public Guid ID { get; set; }
        public string FiscalNumber { get; set; } = string.Empty;
        public string LocalNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public TypeStatusOperationRecorder TypeStatus { get; set; }
        public DateTimeOffset D_REG { get; set; }
        public string Address { get; set; } = string.Empty; 
        public TaxObjectModel? ObjectOwner { get; set; }

        public string StatusString
        {
            get { return OperationRecorderStatusModel.GetTaxObjectStatus().ElementAt(System.Enum.GetValues<TypeStatusOperationRecorder>().ToList().IndexOf(TypeStatus)); }
            set { TypeStatus = System.Enum.GetValues<TypeStatusOperationRecorder>().ToList().ElementAt(OperationRecorderStatusModel.GetTaxObjectStatus().IndexOf(value)); }
        }
    }
}
