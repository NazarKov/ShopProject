using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.OperationRecorderUser
{
    public class BindingUserToOperationRecorderDto
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; } 
    }
}
