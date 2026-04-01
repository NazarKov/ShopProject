using ShopProject.Model.Domain.OperationRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.OperationRecorder
{
    public class OperationRecorderDialogWindowModel
    {
        public Domain.OperationRecorder.OperationRecorder OperationRecorder { get; set; }
        public bool isActive { get; set; }

        public OperationRecorderDialogWindowModel(Domain.OperationRecorder.OperationRecorder operationRecorder)
        {
            this.OperationRecorder = operationRecorder;
            isActive = true;
        }
    }
}
