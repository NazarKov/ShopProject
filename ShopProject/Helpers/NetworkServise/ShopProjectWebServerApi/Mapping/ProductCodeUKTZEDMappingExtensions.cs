using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.ProductCodeUKTZED;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class ProductCodeUKTZEDMappingExtensions
    {
        public static CreateProductUKTZEDDto ToProductCodeUKTZED(this ProductCodeUKTZED item)
        {
            return new CreateProductUKTZEDDto()
            {
                Code = item.Code,
                Status = (int)item.Status,
                NameCode = item.NameCode,
            };
        }
        public static ProductCodeUKTZED ToProductCodeUKTZED(this ProductCodeUKTZEDDto item)
        {
            return new ProductCodeUKTZED()
            {
                ID = item.ID,
                Code = item.Code,
                Status = (TypeStatusCodeUKTZED)item.Status,
                NameCode = item.NameCode,
            };
        }

        public static IEnumerable<ProductCodeUKTZED> ToProductCodeUKTZED(this  IEnumerable<ProductCodeUKTZEDDto> items)
        {
            var result = new List<ProductCodeUKTZED>();
            foreach (var item in items) 
            {
                result.Add(ToProductCodeUKTZED(item));
            }
            return result;
        }
        public static UpdateProductCodeUKTZEDDto ToUpdateProductCodeUKTZED(this ProductCodeUKTZED item)
        {
            return new UpdateProductCodeUKTZEDDto()
            {
                ID = (int)item.ID,
                Code = item.Code,
                Status = (int)item.Status,
                NameCode = item.NameCode,
            };
        }
    }
}
