using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Services.Modules.Mapping.ProductUnit
{
    public static class ProductUnitEntityMappingExtensions
    {
        public static ProductUnitEntity ToProductUnitEntity(this ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit item)
        {
            return new ProductUnitEntity()
            {
                ShortNameUnit = item.ShortNameUnit,
                Status = Enum.Parse<TypeStatusUnit>(item.Status.ToString()),
                ID = item.ID,
                NameUnit = item.NameUnit,
                Number = item.Number,
            };
        }
        public static ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit ToProductUnit(this ProductUnitEntity item)
        {
            return new ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit()
            {
                ShortNameUnit = item.ShortNameUnit,
                Status = Enum.Parse<ShopProjectWebServer.Models.Domain.Enum.TypeStatusUnit>(item.Status.ToString()),
                ID = item.ID,
                NameUnit = item.NameUnit,
                Number = item.Number,
            };
        }
        public static IEnumerable<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit> ToProductUnit(this IEnumerable<ProductUnitEntity> items)
        {
            var result = new List<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit>();
            foreach (var item in items) 
            {
                result.Add(ToProductUnit(item));
            }
            return result;
        }
    }
}
