using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.UIModel.SalePage
{
    public class OperationInfo
    { 
        public decimal TotalCheck { get; set; } 
        public decimal AmountOfFundsIssued { get; set; } 
        public decimal AmountOfFundsReceived { get; set; }
    }
}
