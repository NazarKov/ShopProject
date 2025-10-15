using ShopProject.UIModel.OperationRecorderPage;
using ShopProjectDataBase.Entities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.SalePage
{
    public class MediaAccessControl
    {
        public int ID { get; set; }
        /// <summary>
        /// хешоване значення MAC 
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// порядковий номер MAC
        /// </summary>
        public int SequenceNumber { get; set; }
        /// <summary>
        /// Зміни до яких належить MAC
        /// </summary>
        public WorkingShift? WorkingShifts { get; set; }
        /// <summary>
        /// Операція до яких належить MAC
        /// </summary>
        public Operation? Operation { get; set; }
        /// <summary>
        /// ПРРО який видав MAC
        /// </summary>
        public OperationRecorder? OperationsRecorder { get; set; }
    }
}
