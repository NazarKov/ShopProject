using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.MediaAccessControl
{
    public class CreateMediaAccessControlDto
    {
        public int ID { get; set; }
        /// <summary>
        /// хешоване значення MAC 
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// Зміни до яких належить MAC
        /// </summary>
        public int WorkingShiftsID { get; set; }
        /// <summary>
        /// Операція до яких належить MAC
        /// </summary>
        public int OperationID { get; set; }
        /// <summary>
        /// ПРРО який видав MAC
        /// </summary>
        public Guid OperationsRecorderID { get; set; }
    }
}
