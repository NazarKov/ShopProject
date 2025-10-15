using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class ProductCodeUKTZEDMappingExtensions
    {
        public static ProductCodeUKTZEDEntity ToProductCodeUKTZEDEntity(this CreateProductUKTZEDDto item)
        {
            var result = new ProductCodeUKTZEDEntity()
            {
                Code = item.Code,
                NameCode = item.NameCode
            }; 
            Enum.TryParse(item.Status.ToString(), out TypeStatusCodeUKTZED status);
            result.Status = status;
            return result;
        } 
        public static ProductCodeUKTZEDEntity ToProductCodeUKTZEDEntity(this UpdateProductCodeUKTZEDDto item)
        {
            var result = new ProductCodeUKTZEDEntity()
            {
                ID = item.ID,
                Code = item.Code,
                NameCode = item.NameCode,  
            }; 
            Enum.TryParse(item.Status.ToString(),out TypeStatusCodeUKTZED status); 
            result.Status = status; 
            return result;
        }

        public static ProductCodeUKTZEDDto ToProductCodeUKTZEDDto(this ProductCodeUKTZEDEntity item) 
        {
            return new ProductCodeUKTZEDDto()
            {
                Status = (int)item.Status,
                Code = item.Code,
                NameCode= item.NameCode,
            };
        }
        public static IEnumerable<ProductCodeUKTZEDDto> ToProductCodeUKTZEDDto(this IEnumerable<ProductCodeUKTZEDEntity> items)
        {
            var result = new List<ProductCodeUKTZEDDto>();
            foreach (var item in items) 
            {
                result.Add(ToProductCodeUKTZEDDto(item));
            }
            return result;
        }
    }
}
