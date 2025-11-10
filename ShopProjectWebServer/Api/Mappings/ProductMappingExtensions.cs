using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.DtoModels.Product;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class ProductMappingExtensions
    {
        public static ProductEntity ToProductEntity(this CreateProductDto item)
        {
            var product = new ProductEntity()
            {
                OutStockAt = item.OutStockAt,
                ArhivedAt = item.ArhivedAt,
                Articule = item.Articule,
                Code = item.Code,
                CodeUKTZED = new ProductCodeUKTZEDEntity() { ID = item.CodeUKTZED_ID, },
                Count = item.Count,
                CreatedAt = item.CreatedAt,
                Discount = new DiscountEntity() { ID = item.Discount_ID },
                NameProduct = item.NameProduct,
                Price = item.Price,
                Unit = new ProductUnitEntity() { ID = item.Unit_ID },
            };

            Enum.TryParse(item.Status.ToString(), out TypeStatusProduct statusProduct);
            product.Status = statusProduct;
            return product;
        }
        public static ProductEntity ToProductEntity(this UpdateProductDto item)
        {
            var product = new ProductEntity()
            {
                ID = Guid.Parse(item.ID),
                OutStockAt = item.OutStockAt,
                ArhivedAt = item.ArhivedAt,
                Articule = item.Articule,
                Code = item.Code,
                CodeUKTZED = new ProductCodeUKTZEDEntity() { ID = item.CodeUKTZED_ID, },
                Count = item.Count, 
                Discount = new DiscountEntity() { ID = item.Discount_ID },
                NameProduct = item.NameProduct,
                Price = item.Price,
                Unit = new ProductUnitEntity() { ID = item.Unit_ID },
            }; 
            Enum.TryParse(item.Status.ToString(), out TypeStatusProduct statusProduct);
            product.Status = statusProduct;
            return product;
        }
        public static IEnumerable<ProductEntity> ToProductEnity(this IEnumerable<CreateProductDto> items)
        {
            var products = new List<ProductEntity>();
            foreach (var item in items)
            {
                products.Add(ToProductEntity(item));
            }
            return products;
        }
        public static IEnumerable<ProductEntity> ToProductEntity(this IEnumerable<UpdateProductDto> items)
        {
            var products = new List<ProductEntity>();
            foreach (var item in items)
            {
                products.Add(ToProductEntity(item));
            }
            return products;
        }

        public static ProductDto ToProductDto(this ProductEntity product)
        {
<<<<<<< HEAD
            var item=  new ProductDto()
            {
                ID = product.ID.ToString(),
=======
            return new ProductDto()
            {
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
                Status = (int)product.Status,
                OutStockAt = product.OutStockAt,
                ArhivedAt = product.ArhivedAt,
                Articule = product.Articule,
<<<<<<< HEAD
                Code = product.Code, 
                Count = product.Count,
                CreatedAt = product.CreatedAt, 
                NameProduct = product.NameProduct,
                Price = product.Price, 
            };
            if (product.CodeUKTZED != null)
            {
                item.CodeUKTZED_ID = product.CodeUKTZED.ID;
            }
            if (product.Discount != null)
            {
                item.Discount_ID = product.Discount.ID;
            }
            if (product.Unit != null)
            {
                item.Unit_ID = product.Unit.ID;
            }
            return item;
=======
                Code = product.Code,
                CodeUKTZED_ID = product.CodeUKTZED.ID,
                Count = product.Count,
                CreatedAt = product.CreatedAt,
                Discount_ID = product.Discount.ID,
                NameProduct = product.NameProduct,
                Price = product.Price,
                Unit_ID =product.Unit.ID
            };
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        }

        public static IEnumerable<ProductDto> ToProductDto(this IEnumerable<ProductEntity> items)
        {
            var result  = new List<ProductDto>();   
            foreach(var item in items)
            {
                result.Add(ToProductDto(item));
            }
            return result;
        }
    }
}
