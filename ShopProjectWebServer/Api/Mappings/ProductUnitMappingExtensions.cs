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

        public static ProductUnitDto ToProductUnitDto(this ProductUnitEntity item)
        {
            return new ProductUnitDto()
            { 
                ID = item.ID, 
                ShortNameUnit = item.ShortNameUnit,
                Status = (int)item.Status,
                NameUnit = item.NameUnit,
                Number = item.Number
            };
        }
        public static IEnumerable<ProductUnitDto> ToProductUnitDto(this IEnumerable<ProductUnitEntity> items) 
        {
            var result  = new List<ProductUnitDto>();
            foreach (var item in items) 
            {
                result.Add(ToProductUnitDto(item));
            }
            return result;
        } 
    }
}
