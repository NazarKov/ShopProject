using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Services.Modules.Mapping.ProductCodeUKTZED
{
    public static class ProductCodeUKTZEDEntityMappingExtensions
    {
        public static ProductCodeUKTZEDEntity ToProductCodeUKTZEDEntity(this ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED item)
        {
            return new ProductCodeUKTZEDEntity()
            {
                ID = item.ID,
                Code = item.Code,
                NameCode = item.NameCode,
                Status = Enum.Parse<TypeStatusCodeUKTZED>(item.Status.ToString())
            };
        }
        public static ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED ToProductCodeUKTZED(this ProductCodeUKTZEDEntity item)
        {
            return new ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED()
            {
                ID = item.ID,
                Code = item.Code,
                NameCode = item.NameCode,
                Status = Enum.Parse<ShopProjectWebServer.Models.Domain.Enum.TypeStatusCodeUKTZED>(item.Status.ToString())
            };
        }
        public static IEnumerable<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED> ToProductUKTZED(this IEnumerable<ProductCodeUKTZEDEntity> items)
        {
            var result = new List<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED>();
            foreach(var item in items)
            {
                result.Add(ToProductCodeUKTZED(item));
            }
            return result;
        }
    }
}
