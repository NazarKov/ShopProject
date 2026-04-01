using ShopProject.Model.UI.Operation;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Model.UI.WorkingShift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.MediaAccessControl
{
    internal class MediaAccessControlModel
    {
        public int ID { get; set; }
        public string Content { get; set; } = string.Empty;
        public int SequenceNumber { get; set; }
        public WorkingShiftModel? WorkingShifts { get; set; }
        public OperationModel? Operation { get; set; }
        public OperationRecorderModel? OperationsRecorder { get; set; }
    }
}
