using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Product;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.ProductUnit;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class ProductUnitMappingExtensions
    {
        public static CreateProductUnitDto ToProductUnit(this ProductUnit item)
        {
            return new CreateProductUnitDto()
            {
                ShortNameUnit = item.ShortNameUnit,
                Status = (int)item.Status,
                NameUnit = item.NameUnit,
                Number = item.Number,
            };
        }
        public static UpdateProductUnitDto ToUpdateProductUnit(this ProductUnit item)
        {
            return new UpdateProductUnitDto()
            {
                ID = item.ID,
                ShortNameUnit = item.ShortNameUnit,
                Status = (int)item.Status,
                NameUnit = item.NameUnit,
                Number = item.Number,
            };
        }
        public static ProductUnit ToProductUnit(this ProductUnitDto item)
        {
            return new ProductUnit()
            {
                ID = item.ID,
                ShortNameUnit = item.ShortNameUnit,
                Status = (TypeStatusUnit)item.Status,
                NameUnit = item.NameUnit,
                Number = item.Number,
            };
        }
        public static IEnumerable<ProductUnit> ToProductUnit(this IEnumerable<ProductUnitDto> items) 
        {
            var result = new List<ProductUnit>();
            foreach (var item in items) 
            {
                result.Add(ToProductUnit(item));
            }
            return result;
        }

    }
}
