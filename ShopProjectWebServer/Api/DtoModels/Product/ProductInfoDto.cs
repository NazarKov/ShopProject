using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.DtoModels.Product
{
    public class ProductInfoDto
    {
        public int CountProductAllStatus { get; set; }
        public int CountProductInStockStatus { get; set; }
        public int CountProductOutStockStatus { get; set; }
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
