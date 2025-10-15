using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Product
{
    public class ProductInfoDto
    {
        [JsonPropertyName("CountProductAllStatus")]
        public int CountProductAllStatus { get; set; }
        [JsonPropertyName("CountProductInStockStatus")]
        public int CountProductInStockStatus { get; set; }
        [JsonPropertyName("CountProductOutStockStatus")]
        public int CountProductOutStockStatus { get; set; }
        [JsonPropertyName("CountProductArchivedStauts")]
        public int CountProductArchivedStauts { get; set; }
    }
}
