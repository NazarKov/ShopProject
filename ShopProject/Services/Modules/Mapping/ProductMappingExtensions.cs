using ShopProject.Model.Domain.Product;
using ShopProject.Model.UI.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.MappingServise
{
    public static class ProductMappingExtensions
    {
        public static ProductModel ToProductModel(this Product item)
        {
            var result = new ProductModel()
            {
                ID = item.ID, 
                Status = item.Status,
                Articule = item.Articule,
                Code = item.Code,
                Count = item.Count,
                NameProduct = item.NameProduct,
                Price = item.Price,
            };
            if (item.CodeUKTZED != null)
            {
                result.CodeUKTZED = item.CodeUKTZED.ToProductCodeUKTZEDModel();
            }
            if (item.Unit != null)
            {
                result.Unit = item.Unit.ToProductUnitModel();
            }
            if (item.Discount != null)
            {
                result.Discount = item.Discount.ToDiscountModel();
            }

            return result;
        }

        public static IEnumerable<ProductModel> ToProductModel(this IEnumerable<Product> items)
        {
            var result = new List<ProductModel>();
            foreach (var item in items)
            {
                result.Add(ToProductModel(item));
            }
            return result;
        }

        public static Product ToProduct(this ProductModel item)
        {
            var result = new Product()
            {
                ID = item.ID,
                ArhivedAt = null,
                Status = item.Status,
                Articule = item.Articule,
                Code = item.Code,
                Count = item.Count,
                NameProduct = item.NameProduct,
                Price = item.Price,
            };
            if (item.CodeUKTZED != null)
            {
                result.CodeUKTZED = item.CodeUKTZED.ToProductCodeUKTZED();
            }
            if (item.Unit != null)
            {
                result.Unit = item.Unit.ToProductUnit();
            }
            if (item.Discount != null)
            {
                result.Discount = item.Discount.ToDiscount();
            }
            return result;
        }

        public static IEnumerable<Product> ToProduct(this IEnumerable<ProductModel> items)
        {
            var result = new List<Product>();
            foreach (var item in items)
            {
                result.Add(ToProduct(item));
            }
            return result;
        }
    }
}
