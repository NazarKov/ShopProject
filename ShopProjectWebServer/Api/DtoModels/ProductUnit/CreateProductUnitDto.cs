using ShopProjectDataBase.Helper;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.ProductUnit
{
    public class CreateProductUnitDto
    {
        [JsonPropertyName("NameUnit")]
        public string NameUnit { get; set; } = string.Empty;
        [JsonPropertyName("ShortNameUnit")]
        public string ShortNameUnit { get; set; } = string.Empty;
        [JsonPropertyName("Number")]
        public int Number { get; set; } = 0;
        [JsonPropertyName("Status")]
        public int Status { get; set; }
    }
}
