using ShopProjectWebServer.Api.DtoModels.Order;
using ShopProjectWebServer.Api.DtoModels.Product;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.Operation
{
    public class OperationІnformationDto
    {
        [JsonPropertyName("Operation")]
        public OperationDto? Operation { get; set; }
        [JsonPropertyName("Products")]
        public IEnumerable<ProductDto>? Products { get; set; }
    }
}
