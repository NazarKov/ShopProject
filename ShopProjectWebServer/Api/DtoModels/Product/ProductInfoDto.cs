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


        public static ProductInfoDto Create(IEnumerable<ProductEntity> products)
        {
            return new ProductInfoDto()
            {
                CountProductAllStatus = products.Count(),
                CountProductInStockStatus = products.Where(i => i.Status == TypeStatusProduct.InStock).Count(),
                CountProductOutStockStatus = products.Where(i => i.Status == TypeStatusProduct.OutStock).Count(),
                CountProductArchivedStauts = products.Where(i => i.Status == TypeStatusProduct.Archived).Count(),
            };
        }
    }
}
