using ShopProject.Model.Domain.Discount; 
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.Product
{
    public static class ApiProductMappingExtensions
    {
        public static ShopProject.Model.Domain.Product.Product ToProduct(this ProductDto item,IEnumerable<ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED> codesUKTZED , IEnumerable<ShopProject.Model.Domain.ProductUnit.ProductUnit> productUnits)
        {
            var result = new ShopProject.Model.Domain.Product.Product()
            {
                ID = Guid.Parse(item.ID),
                Status = (TypeStatusProduct)item.Status,
                OutStockAt = item.OutStockAt,
                ArhivedAt = item.ArhivedAt,
                Articule = item.Articule,
                Code = item.Code,
                Count = item.Count,
                CreatedAt = item.CreatedAt,
                NameProduct = item.NameProduct,
                Price = item.Price,
            };
            if (codesUKTZED != null && codesUKTZED.Count() > 0)
            {
                if (item.CodeUKTZED_ID != 0)
                {
                    result.CodeUKTZED = codesUKTZED.First(i => i.ID == item.CodeUKTZED_ID);
                }
            }
            if (productUnits != null && productUnits.Count() > 0)
            {
                if (item.Unit_ID != 0)
                {
                    result.Unit = productUnits.First(i => i.ID == item.Unit_ID);
                }
            }
            result.Discount = new ShopProject.Model.Domain.Discount.Discount();
            return result;
        }
        public static IEnumerable<ShopProject.Model.Domain.Product.Product> ToProduct(this IEnumerable<ProductDto> items, IEnumerable<ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED> codesUKTZED, 
            IEnumerable<ShopProject.Model.Domain.ProductUnit.ProductUnit> productUnits)
        {
            var result = new List<ShopProject.Model.Domain.Product.Product>();
            foreach (var item in items) 
            {
                result.Add(ToProduct(item, codesUKTZED , productUnits));
            }
            return result;
        }

        public static ProductsInfo ToProductsInfo(this ProductInfoDto item) 
        {
            return new ProductsInfo() {
                CountProductAllStatus = item.CountProductAllStatus,
                CountProductArchivedStauts = item.CountProductArchivedStauts,
                CountProductOutStockStatus = item.CountProductOutStockStatus,
                CountProductInStockStatus = item.CountProductInStockStatus,
            };
        }
        public static UpdateProductDto ToUpdateProductDto(this ShopProject.Model.Domain.Product.Product item)
        {
            var product = new UpdateProductDto()
            {
                ID = item.ID.ToString(),
                Status = (int)item.Status,
                OutStockAt = item.OutStockAt,
                Articule = item.Articule,
                ArhivedAt = item.ArhivedAt,
                Code = item.Code, 
                Count = item.Count, 
                NameProduct = item.NameProduct,
                Price = item.Price, 
            };
            if (item.CodeUKTZED != null)
            {
                product.CodeUKTZED_ID = item.CodeUKTZED.ID;
            }
            if (item.Discount != null)
            {
                product.Discount_ID = item.Discount.ID;
            }
            if (item.Unit != null)
            {
                product.Unit_ID = item.Unit.ID;
            }
            return product;
        }
        public static IEnumerable<UpdateProductDto> ToUpdateProductDto(this List<ShopProject.Model.Domain.Product.Product> items)
        {
            var result = new List<UpdateProductDto>();
            foreach (var item in items)
            {
                result.Add(ToUpdateProductDto(item));
            }
            return result;
        }

        public static CreateProductDto ToCreateProductDto(this ShopProject.Model.Domain.Product.Product item)
        {
            var product = new CreateProductDto()
            {
                Status = (int)item.Status,
                OutStockAt  = item.OutStockAt,
                ArhivedAt = item.ArhivedAt,
                Articule= item.Articule,
                Code = item.Code, 
                Count= item.Count,
                CreatedAt = item.CreatedAt, 
                NameProduct= item.NameProduct,
                Price= item.Price, 
            };
            if (item.CodeUKTZED != null)
            {
                product.CodeUKTZED_ID = item.CodeUKTZED.ID;
            }
            if (item.Discount != null)
            {
                product.Discount_ID = item.Discount.ID;
            }
            if (item.Unit != null)
            {
                product.Unit_ID = item.Unit.ID;
            }
            return product;
        }
        public static IEnumerable<CreateProductDto> ToCreateProductDto(this IEnumerable<ShopProject.Model.Domain.Product.Product> items)
        {
            var products = new List<CreateProductDto>();
            foreach (var item in items) 
            {
                products.Add(ToCreateProductDto(item));
            }
            return products;
        }
    }
}
