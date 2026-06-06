using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Models.Domain.Enum;

namespace ShopProjectWebServer.Services.Modules.Mapping.Product
{
    public static class ProductApiMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.Product.Product ToProduct(this CreateProductDto item)
        {
            return new Models.Domain.Product.Product()
            {
                Status = Enum.Parse<TypeStatusProduct>(item.Status.ToString()),
                Articule = item.Articule,
                Code = item.Code,
                Count = item.Count,
                CreatedAt = item.CreatedAt,
                NameProduct = item.NameProduct,
                Price = item.Price,
                Unit = new Models.Domain.ProductUnit.ProductUnit() { ID = item.Unit_ID },
                CodeUKTZED = new Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED() { ID = item.CodeUKTZED_ID },
                Discount = new Models.Domain.Discount.Discount() { ID = item.Discount_ID }, 
            };
        }

        public static IEnumerable<ShopProjectWebServer.Models.Domain.Product.Product> ToProduct(this IEnumerable<CreateProductDto> items)
        {
            var result = new List<ShopProjectWebServer.Models.Domain.Product.Product>();
            foreach (var item in items) 
            {
                result.Add(ToProduct(item));
            }
            return result;
        }

        public static ProductDto ToProductDto (this ShopProjectWebServer.Models.Domain.Product.Product item)
        {
            var result = new ProductDto()
            {
                ID = item.ID.ToString(),
                ArhivedAt = item.ArhivedAt,
                Articule = item.Articule,
                Code = item.Code,
                Count = item.Count,
                CreatedAt = item.CreatedAt,
                Price = item.Price,
                OutStockAt = item.OutStockAt,
                NameProduct = item.NameProduct,
                Status = (int)item.Status,
            };
            if(item.Discount != null)
            {
                result.Discount_ID = item.Discount.ID;
            }
            if (item.Unit != null) 
            {
                result.Unit_ID = item.Unit.ID;
            }
            if (item.CodeUKTZED != null) 
            {
                result.CodeUKTZED_ID = item.CodeUKTZED.ID;
            }
            return result;
        }

        public static IEnumerable<ProductDto> ToProductDto(this IEnumerable<ShopProjectWebServer.Models.Domain.Product.Product> items)
        {
            var result = new List<ProductDto>();
            foreach (var item in items)
            {
                result.Add(ToProductDto(item));
            }
            return result;
        }

        public static ShopProjectWebServer.Models.Domain.Product.Product ToProduct(this UpdateProductDto item)
        {
            return new Models.Domain.Product.Product()
            {
                ID = Guid.Parse(item.ID),
                Status = Enum.Parse<TypeStatusProduct>(item.Status.ToString()),
                Articule = item.Articule,
                Code = item.Code,
                Count = item.Count, 
                NameProduct = item.NameProduct,
                Price = item.Price,
                Unit = new Models.Domain.ProductUnit.ProductUnit() { ID = item.Unit_ID },
                CodeUKTZED = new Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED() { ID = item.CodeUKTZED_ID },
                Discount = new Models.Domain.Discount.Discount() { ID = item.Discount_ID },
                OutStockAt = item.OutStockAt,
                ArhivedAt = item.ArhivedAt, 
            };
        }

        public static IEnumerable<ShopProjectWebServer.Models.Domain.Product.Product> ToProduct(this IEnumerable<UpdateProductDto> items)
        {
            var result = new List<ShopProjectWebServer.Models.Domain.Product.Product>();
            foreach (var item in items)
            {
                result.Add(ToProduct(item));
            }
            return result;
        }

        public static ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.Product.Product,int> ToPaginator(this PaginatorDto<ProductDto, int> paginator)
        {
            return new Models.Domain.Paginator.Paginator<Models.Domain.Product.Product, int>()
            {
                CountItemPage = paginator.CountItemPage, 
                DataType = paginator.DataType,
                Page = paginator.Page,
                Pages = paginator.Pages,
            };
        }

        public static PaginatorDto<ProductDto, int> ToPaginatorDto(this ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.Product.Product, int> paginator)
        {
            var result = new PaginatorDto<ProductDto, int>
            { 
                CountItemPage = paginator.CountItemPage,
                DataType = paginator.DataType,
                Page = paginator.Page,
                Pages = paginator.Pages,
            };
            if(paginator.Data != null)
            {
                result.Data = ToProductDto(paginator.Data);
            }
            return result;
        }

    }
}
