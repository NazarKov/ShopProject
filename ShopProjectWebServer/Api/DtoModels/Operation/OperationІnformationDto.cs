using ShopProjectWebServer.Api.DtoModels.Discount;
using ShopProjectWebServer.Api.DtoModels.Order;
using ShopProjectWebServer.Api.DtoModels.Product;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.Operation
{
    public class OperationІnformationDto
    {
        [JsonPropertyName("Operation")]
        public OperationDto? Operation { get; set; }
        [JsonPropertyName("Discount")]
        public DiscountDto? Discount { get; set; }
        [JsonPropertyName("Products")]
        public IEnumerable<ProductDto>? Products { get; set; }
    }
}
