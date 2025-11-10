using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
<<<<<<< HEAD
using System.Text.Json.Serialization;
=======
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

namespace ShopProjectWebServer.Api.DtoModels.Product
{
    public class ProductInfoDto
    {
<<<<<<< HEAD
        [JsonPropertyName("CountProductAllStatus")]
        public int CountProductAllStatus { get; set; }
        [JsonPropertyName("CountProductInStockStatus")]
        public int CountProductInStockStatus { get; set; }
        [JsonPropertyName("CountProductOutStockStatus")]
        public int CountProductOutStockStatus { get; set; }
        [JsonPropertyName("CountProductArchivedStauts")]
=======
        public int CountProductAllStatus { get; set; }
        public int CountProductInStockStatus { get; set; }
        public int CountProductOutStockStatus { get; set; }
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
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
