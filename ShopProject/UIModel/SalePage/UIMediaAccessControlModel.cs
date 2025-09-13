using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.SalePage
{
    public class UIMediaAccessControlModel
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
        public UIWorkingShiftModel? WorkingShifts { get; set; }
        /// <summary>
        /// Операція до яких належить MAC
        /// </summary>
        public OperationEntity? Operation { get; set; }
        /// <summary>
        /// ПРРО який видав MAC
        /// </summary>
        public OperationsRecorderEntity? OperationsRecorder { get; set; }
    }
}
