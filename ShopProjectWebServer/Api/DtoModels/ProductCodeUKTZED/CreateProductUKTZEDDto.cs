using ShopProjectDataBase.Helper;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED
{
    public class CreateProductUKTZEDDto
    {
        [JsonPropertyName("NameCode")]
        public string NameCode { get; set; } = string.Empty;
        [JsonPropertyName("Code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("Status")]
        public int Status { get; set; }
    }
}
