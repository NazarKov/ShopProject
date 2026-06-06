using ShopProjectWebServer.Api.DtoModels.Product;

namespace ShopProjectWebServer.Services.Modules.Mapping.Product
{
    public static class ProductInfoApiMappingExtensions
    {
        public static ProductInfoDto ToProductInfo(this ShopProjectWebServer.Models.Domain.Product.ProductsInfo item)
        {
            return new ProductInfoDto()
            {
                CountProductAllStatus = item.CountProductAllStatus,
                CountProductArchivedStauts = item.CountProductArchivedStauts,
                CountProductInStockStatus = item.CountProductInStockStatus,
                CountProductOutStockStatus = item.CountProductOutStockStatus,
            };
        }
    }
}
