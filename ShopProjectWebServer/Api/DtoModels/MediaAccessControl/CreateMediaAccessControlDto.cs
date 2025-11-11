
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.MediaAccessControl
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
