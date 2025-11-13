using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Product;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class ProductMappingExtensions
    {
        public static Product ToProduct(this ProductDto item)
        {
            var result = new Product()
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
            if (Session.ProductCodesUKTZED != null && Session.ProductCodesUKTZED.Count() > 0)
            {
                result.CodeUKTZED = Session.ProductCodesUKTZED.FirstOrDefault(i => i.ID == item.CodeUKTZED_ID);
            }
            if (Session.ProductUnits != null && Session.ProductUnits.Count() > 0)
            {
                result.Unit = Session.ProductUnits.FirstOrDefault(i => i.ID == item.Unit_ID);
            }
            result.Discount = new Discount();
            return result;
        }
        public static IEnumerable<Product> ToProduct(this IEnumerable<ProductDto> items)
        {
            var result = new List<Product>();
            foreach (var item in items) 
            {
                result.Add(ToProduct(item));
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
        public static UpdateProductDto ToUpdateProductDto(this Product item)
        {
            return new UpdateProductDto()
            {
                ID = item.ID.ToString(),
                Status = (int)item.Status,
                OutStockAt = item.OutStockAt,
                Articule = item.Articule,
                ArhivedAt = item.ArhivedAt,
                Code = item.Code,
                CodeUKTZED_ID = item.CodeUKTZED.ID,
                Count = item.Count, 
                Discount_ID = item.Discount.ID,
                NameProduct = item.NameProduct,
                Price = item.Price,
                Unit_ID = item.Unit.ID,
            };
        }

        public static CreateProductDto ToCreateProductDto(this Product item)
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
        public static IEnumerable<CreateProductDto> ToCreateProductDto(this IEnumerable<Product> items)
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
