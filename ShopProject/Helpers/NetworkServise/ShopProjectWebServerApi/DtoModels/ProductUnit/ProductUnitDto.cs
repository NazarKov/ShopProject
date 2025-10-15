using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.ProductUnit
{
    public class ProductUnitDto
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
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
