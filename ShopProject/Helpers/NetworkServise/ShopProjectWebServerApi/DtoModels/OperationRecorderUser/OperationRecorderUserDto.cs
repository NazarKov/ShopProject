using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.OperationRecorder;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.OperationRecorderUser
{
    public class OperationRecorderUserDto
    {
        [JsonPropertyName("User")]
        public UserDto? User { get; set; }
        [JsonPropertyName("OpertionsRecorders")]
        public IEnumerable<OperationRecorderDto>? OpertionsRecorders { get; set; }
    }
}
