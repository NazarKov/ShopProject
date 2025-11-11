using ShopProjectDataBase.Entities; 
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.DtoModels.User; 
using System.Text.Json.Serialization;  

namespace ShopProjectWebServer.Api.DtoModels.OperationRecorderUser
{
    public class OperationRecorderUserDto
    {  
        [JsonPropertyName("User")]
        public UserDto? User { get; set; }
        [JsonPropertyName("OpertionsRecorders")]
        public IEnumerable<OperationRecorderDto>? OpertionsRecorders { get; set; } 
    }
}
