using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;
using ShopProjectWebServer.Models.Domain.Enum;
using ShopProjectWebServer.Models.Domain.ProductCodeUKTZED;

namespace ShopProjectWebServer.Services.Modules.Mapping.ProductCodeUKTZED
{
    public static class ProductCodeUKTEZEDApiMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED ToProductCodeUKTZED(this CreateProductUKTZEDDto item)
        {
            return new Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED()
            {
                Status = (TypeStatusCodeUKTZED)item.Status,
                Code = item.Code,
                NameCode = item.NameCode,
            };
        }

        public static ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED ToProductCodeUKTZED(this UpdateProductCodeUKTZEDDto item)
        {
            return new Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED()
            {
                ID = item.ID,
                Status = (TypeStatusCodeUKTZED)item.Status,
                Code = item.Code,
                NameCode = item.NameCode,
            };
        }

        public static ProductCodeUKTZEDDto ToProductCodeUKTZEDDto(this ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED item)
        {
            return new ProductCodeUKTZEDDto()
            {
                ID = item.ID,
                Status = (int)item.Status,
                Code = item.Code,
                NameCode = item.NameCode,
            };
        }
        public static IEnumerable<ProductCodeUKTZEDDto> ToProductCodeUKTZEDDto(this IEnumerable<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED> items)
        {
            var result = new List<ProductCodeUKTZEDDto>();
            foreach (var item in items) 
            {
                result.Add(ToProductCodeUKTZEDDto(item));
            }
            return result;
        }

        public static ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED, int> ToPaginator(this PaginatorDto<ProductCodeUKTZEDDto, int> paginator)
        {
            return new ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED, int>()
            {
                CountItemPage = paginator.CountItemPage,
                DataType = paginator.DataType,
                Page = paginator.Page,
                Pages = paginator.Pages,
            };
        }
        public static PaginatorDto<ProductCodeUKTZEDDto, int> ToPaginatorDto(this ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED, int> paginator)
        {
            var result = new PaginatorDto<ProductCodeUKTZEDDto, int>()
            {
                CountItemPage = paginator.CountItemPage,
                DataType = paginator.DataType,
                Page = paginator.Page,
                Pages = paginator.Pages,
            };
            if (paginator.Data != null)
            {
                result.Data = ToProductCodeUKTZEDDto(paginator.Data);
            }
            return result;
        }
    }
}
