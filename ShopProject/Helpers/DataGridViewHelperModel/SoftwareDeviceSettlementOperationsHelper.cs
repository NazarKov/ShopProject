using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers
{
    public class SoftwareDeviceSettlementOperationsHelper
    {
        public OperationsRecorderEntity deviceSettlementOperations { get; set; }
        public bool isActive { get; set; }

        public SoftwareDeviceSettlementOperationsHelper(OperationsRecorderEntity item)
        {
            this.deviceSettlementOperations = item;
            isActive = true;
        }
    }
}
