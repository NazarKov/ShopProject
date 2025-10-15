using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.OperationRecorderPage
{
    public class OperationRecorderDialogWindow
    {
        public OperationRecorder deviceSettlementOperations { get; set; }
        public bool isActive { get; set; }

        public OperationRecorderDialogWindow(OperationRecorder item)
        {
            this.deviceSettlementOperations = item;
            isActive = true;
        }
    }
}
