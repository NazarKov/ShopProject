using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.WorkingShift
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
