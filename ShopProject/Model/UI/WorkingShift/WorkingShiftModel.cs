using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.MediaAccessControl;
using ShopProject.Model.UI.Operation;
using ShopProject.Model.UI.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.WorkingShift
{
    internal class WorkingShiftModel
    {
        public int ID { get; set; }
        public string FiscalNumberRRO { get; set; } = string.Empty;
        public string FactoryNumberRRO { get; set; } = string.Empty;
        public decimal DataPacketIdentifier { get; set; } = decimal.Zero;
        public decimal TypeRRO { get; set; } = decimal.Zero;
        public TypeWorkingShift TypeShiftCrateAt { get; set; }
        public TypeWorkingShift TypeShiftEndAt { get; set; }
        public decimal TotalCheckForShift { get; set; } = decimal.Zero;
        public decimal TotalReturnCheckForShift { get; set; } = decimal.Zero;
        public decimal AmountOfOfficialFundsReceivedCash { get; set; } = decimal.Zero;
        public decimal AmountOfOfficialFundsIssuedCash { get; set; } = decimal.Zero;
        public decimal AmountOfOfficialFundsReceivedCard { get; set; } = decimal.Zero;
        public decimal AmountOfOfficialFundsIssuedCard { get; set; } = decimal.Zero;
        public decimal AmountOfFundsReceived { get; set; } = decimal.Zero;
        public decimal AmountOfFundsIssued { get; set; } = decimal.Zero; 
        public MediaAccessControlModel? MACCreateAt { get; set; } 
        public MediaAccessControlModel? MACEndAt { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public DateTimeOffset EndAt { get; set; }
        public UserModel? UserOpenShift { get; set; }
        public UserModel? UserCloseShift { get; set; } 
        public IEnumerable<OperationModel>? Operations { get; set; }
    }
}
