using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.ProductCodeUKTZED
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
