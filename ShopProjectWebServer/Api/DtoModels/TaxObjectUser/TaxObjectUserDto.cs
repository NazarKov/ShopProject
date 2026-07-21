using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.DtoModels.TaxObject;
using ShopProjectWebServer.Api.DtoModels.User;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.TaxObjectUser
{
    public class TaxObjectUserDto
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("User")]
        public UserDto? User { get; set; }
        [JsonPropertyName("TaxObject")]
        public TaxObjectDto? TaxObject { get; set; }
        [JsonPropertyName("OperationRecorder")]
        public IEnumerable<OperationRecorderDto>? OperationRecorder { get; set; }
    }
}
