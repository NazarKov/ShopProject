using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDataBase.Entities
{
    public class MediaAccessControlEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// хешоване значення MAC 
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// порядковий номер MAC
        /// </summary>
        public int SequenceNumber {  get; set; }
        /// <summary>
        /// Зміни до яких належить MAC
        /// </summary>
        public WorkingShiftEntity? WorkingShifts { get; set; }
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
