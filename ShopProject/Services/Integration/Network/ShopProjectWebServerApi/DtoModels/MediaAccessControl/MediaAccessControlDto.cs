using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.MediaAccessControl
{
    public class MediaAccessControlDto
    {
        [JsonPropertyName("Content")]
        public string Content { get; set; } = string.Empty;
        [JsonPropertyName("SequenceNumber")]
        public int SequenceNumber { get; set; }
    }
}
