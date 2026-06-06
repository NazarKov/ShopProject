using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using System.Text.Json.Serialization; 

namespace ShopProjectWebServer.Api.DtoModels.Product
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
