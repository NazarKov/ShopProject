using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.User; 
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.WorkingShift
{
    public class WorkingShift
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
        [JsonIgnore]
        public MediaAccessControl.MediaAccessControl? MACCreateAt { get; set; } 
        [JsonIgnore]
        public MediaAccessControl.MediaAccessControl? MACEndAt { get; set; } 
        public DateTimeOffset CreateAt { get; set; } 
        public DateTimeOffset EndAt { get; set; } 
        public User.User? UserOpenShift { get; set; } 
        public User.User? UserCloseShift { get; set; } 
        [JsonIgnore]
        public IEnumerable<Operation.Operation>? Operations { get; set; }
    }
}
