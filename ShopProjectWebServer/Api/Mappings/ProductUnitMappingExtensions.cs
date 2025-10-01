using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Api.DtoModels.ProductUnit;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class ProductUnitMappingExtensions
    {
        public static ProductUnitEntity ToProductUnitEntity(this CreateProductUnitDto item)
        {
            var productUnitEntity = new ProductUnitEntity()
            {
                NameUnit = item.NameUnit,
                ShortNameUnit = item.ShortNameUnit,
                Number = item.Number,
            };

            Enum.TryParse(item.Status.ToString(), out TypeStatusUnit type);
            productUnitEntity.Status = type;
            return productUnitEntity;
        }

        public static ProductUnitEntity ToProductUnitEntity(this UpdateProductUnitDto item)
        {
            var productUnitEntity = new ProductUnitEntity()
            {
                ID = item.ID,
                NameUnit = item.NameUnit,
                ShortNameUnit = item.ShortNameUnit,
                Number = item.Number,
            };

            Enum.TryParse(item.Status.ToString(), out TypeStatusUnit type);
            productUnitEntity.Status = type;
            return productUnitEntity;
        }

    }
}
