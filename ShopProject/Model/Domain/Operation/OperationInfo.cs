using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.Operation
{
    public class OperationInfo
    {
        public decimal TotalCheck { get; set; }
        public decimal AmountOfFundsIssued { get; set; }
        public decimal AmountOfFundsReceived { get; set; }
        public decimal AmountOfOfficialFundsIssued { get; set; }
        public decimal AmountOfOfficialFundsReceived { get; set; }
        public decimal TotalReturnCheck { get; set; }
    }
}
