using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.MediaAccessControl
{
    public class MediaAccessControlDto
    {
        /// <summary>
        /// хешоване значення MAC 
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// порядковий номер MAC
        /// </summary>
        public int SequenceNumber { get; set; }
    }
}
