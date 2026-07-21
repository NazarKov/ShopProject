using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.MediaAccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.DtoModels.WorkingShift
{
    public class WorkingShiftResourseDto
    {
        [JsonPropertyName("ID")]
        public int ID;
        [JsonPropertyName("MediaAccessControl")]
        public MediaAccessControlDto MediaAccessControl { get; set; }
        [JsonPropertyName("OperationNumber")]
        public string OperationNumber { get; set; }
    }
}
