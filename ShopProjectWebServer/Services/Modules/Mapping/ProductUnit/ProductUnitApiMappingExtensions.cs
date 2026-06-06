using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ProductUnit;
using ShopProjectWebServer.Models.Domain.Enum;

namespace ShopProjectWebServer.Services.Modules.Mapping.ProductUnit
{
    public static class ProductUnitApiMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit ToProductUnit(this CreateProductUnitDto item)
        {
            return new Models.Domain.ProductUnit.ProductUnit()
            {
                NameUnit = item.NameUnit,
                Number = item.Number,
                ShortNameUnit = item.ShortNameUnit,
                Status = (TypeStatusUnit)item.Status,
            };
        }

        public static ProductUnitDto ToProductUnit(this ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit item)
        {
            return new ProductUnitDto()
            {
                ID = item.ID,
                NameUnit = item.NameUnit,
                Number = item.Number,
                ShortNameUnit = item.ShortNameUnit,
                Status = (int)item.Status,
            };
        }

        public static IEnumerable<ProductUnitDto> ToProductUnit(this IEnumerable<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit> items)
        {
            var result = new List<ProductUnitDto>();
            foreach (var item in items) 
            {
                result.Add(ToProductUnit(item));
            }
            return result;
        }

        public static ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit ToProductUnit(this UpdateProductUnitDto item)
        {
            return new Models.Domain.ProductUnit.ProductUnit()
            {
                ID = item.ID,
                NameUnit = item.NameUnit,
                Number = item.Number,
                ShortNameUnit = item.ShortNameUnit,
                Status = (TypeStatusUnit)item.Status,
            };
        }

        public static ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit, int> ToPaginator(this PaginatorDto<ProductUnitDto, int> item)
        {
            return new Models.Domain.Paginator.Paginator<Models.Domain.ProductUnit.ProductUnit, int>()
            {
                CountItemPage = item.CountItemPage,
                DataType = item.DataType,
                Page = item.Page,
                Pages = item.Pages,
            };
        }
        public static PaginatorDto<ProductUnitDto, int> ToPaginatorDto(this ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit, int> item)
        {
            var result = new PaginatorDto<ProductUnitDto, int>()
            {
                CountItemPage = item.CountItemPage,
                DataType = item.DataType,
                Page = item.Page,
                Pages = item.Pages,
            };
            if (item.Data != null)
            {
                result.Data = item.Data.ToProductUnit();
            }
            return result;
        }
    }
}
