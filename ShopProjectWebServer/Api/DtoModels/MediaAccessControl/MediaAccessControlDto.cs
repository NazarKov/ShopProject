using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.MediaAccessControl
{
    public class MediaAccessControlDto
    {
        [JsonPropertyName("Content")]
        public string Content { get; set; } = string.Empty;
        [JsonPropertyName("SequenceNumber")]
        public int SequenceNumber { get; set; }
    }
}
