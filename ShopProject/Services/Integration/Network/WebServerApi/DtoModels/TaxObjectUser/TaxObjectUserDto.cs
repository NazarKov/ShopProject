using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorder;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.User;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.TaxObject;  
using System.Collections.Generic; 
using System.Text.Json.Serialization; 

namespace ShopProject.Services.Integration.Network.WebServerApi.DtoModels.TaxObjectUser
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
