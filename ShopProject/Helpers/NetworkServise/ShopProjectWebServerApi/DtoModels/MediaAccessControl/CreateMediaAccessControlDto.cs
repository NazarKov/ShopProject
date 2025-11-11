using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.MediaAccessControl
{
    public class CreateMediaAccessControlDto
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("Content")]
        public string Content { get; set; } = string.Empty;
        [JsonPropertyName("WorkingShiftsID")]
        public int WorkingShiftsID { get; set; }
        [JsonPropertyName("OperationID")]
        public int OperationID { get; set; }
        [JsonPropertyName("OperationsRecorderID")]
        public Guid OperationsRecorderID { get; set; }
    }
}
