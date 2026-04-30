using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.ControlWebServer
{
    public class ControlWebServerDto
    {
        [JsonPropertyName("IsEnabled")]
        public bool IsEnabled { get; set; }
        [JsonPropertyName("IsEnableDataBase")]
        public bool IsEnableDataBase {  get; set; }
    }
}
