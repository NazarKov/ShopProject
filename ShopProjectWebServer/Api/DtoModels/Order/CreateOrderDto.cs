using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.Order
{
    public class CreateOrderDto
    {
        [JsonPropertyName("Count")]
        public int Count { get; set; } = 0;
        [JsonPropertyName("ProductID")]
        public string ProductID { get; set; }
        [JsonPropertyName("OperationID")]
        public int OperationID { get; set; }
    }
}
