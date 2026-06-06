using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Services.Modules.Mapping.Discount;
using ShopProjectWebServer.Services.Modules.Mapping.ProductCodeUKTZED;
using ShopProjectWebServer.Services.Modules.Mapping.ProductUnit;

namespace ShopProjectWebServer.Services.Modules.Mapping.Product
{
    public static class ProductEntityMappingExtensions
    {
        public static ProductEntity ToProductEntity(this ShopProjectWebServer.Models.Domain.Product.Product item)
        {
            var result = new ProductEntity()
            {
                ID = item.ID,
                ArhivedAt = item.ArhivedAt,
                Articule = item.Articule,
                Code = item.Code,
                Count = item.Count,
                CreatedAt = item.CreatedAt,
                NameProduct = item.NameProduct,
                OutStockAt = item.OutStockAt,
                Price = item.Price,
                Status = Enum.Parse<TypeStatusProduct>(item.Status.ToString())
            };
            if (item.Discount != null)
            {
                result.Discount = item.Discount.ToDiscountEntity();
            }
            if (item.Unit != null)
            {
                result.Unit = item.Unit.ToProductUnitEntity();
            }

            if (item.CodeUKTZED != null)
            {
                result.CodeUKTZED = item.CodeUKTZED.ToProductCodeUKTZEDEntity();
            }
            return result;

        }
        public static IEnumerable<ShopProjectWebServer.Models.Domain.Product.Product> ToProduct(this IEnumerable<ProductEntity> items)
        {
            var result = new List<ShopProjectWebServer.Models.Domain.Product.Product>();
            foreach (var item in items) 
            {
                result.Add(ToProduct(item));
            }
            return result;
        }

        public static ShopProjectWebServer.Models.Domain.Product.Product ToProduct(this ProductEntity item)
        {
            var result = new ShopProjectWebServer.Models.Domain.Product.Product()
            {
                ID = item.ID,
                ArhivedAt = item.ArhivedAt,
                Articule = item.Articule,
                Code = item.Code,
                Count = item.Count,
                CreatedAt = item.CreatedAt,
                NameProduct = item.NameProduct,
                OutStockAt = item.OutStockAt,
                Price = item.Price,
                Status = Enum.Parse<ShopProjectWebServer.Models.Domain.Enum.TypeStatusProduct>(item.Status.ToString())
            };
            if (item.Discount != null)
            {
                result.Discount = item.Discount.ToDiscount();
            }
            if (item.Unit != null)
            {
                result.Unit = item.Unit.ToProductUnit();
            }

            if (item.CodeUKTZED != null)
            {
                result.CodeUKTZED = item.CodeUKTZED.ToProductCodeUKTZED();
            }
            return result;

        }

        public static IEnumerable<ProductEntity> ToProductEntity(this IEnumerable<ShopProjectWebServer.Models.Domain.Product.Product> items)
        {
            var result = new List<ProductEntity>();
            foreach (var item in items) 
            {
                result.Add(ToProductEntity(item));
            }
            return result;
        }
    }
}
