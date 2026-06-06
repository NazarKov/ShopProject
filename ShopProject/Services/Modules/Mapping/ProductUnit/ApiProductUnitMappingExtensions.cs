using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ProductUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.ProductUnit
{
    public static class ApiProductUnitMappingExtensions
    {
        public static CreateProductUnitDto ToCreateProductUnitDto(this ShopProject.Model.Domain.ProductUnit.ProductUnit item)
        {
            return new CreateProductUnitDto()
            {
                ShortNameUnit = item.ShortNameUnit,
                Status = (int)item.Status,
                NameUnit = item.NameUnit,
                Number = item.Number,
            };
        }
        public static UpdateProductUnitDto ToUpdateProductUnitDto(this ShopProject.Model.Domain.ProductUnit.ProductUnit item)
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
        public static ShopProject.Model.Domain.ProductUnit.ProductUnit ToProductUnit(this ProductUnitDto item)
        {
            return new ShopProject.Model.Domain.ProductUnit.ProductUnit()
            {
                ID = item.ID,
                ShortNameUnit = item.ShortNameUnit,
                Status = (TypeStatusUnit)item.Status,
                NameUnit = item.NameUnit,
                Number = item.Number,
            };
        }
        public static IEnumerable<ShopProject.Model.Domain.ProductUnit.ProductUnit> ToProductUnit(this IEnumerable<ProductUnitDto> items) 
        {
            var result = new List<ShopProject.Model.Domain.ProductUnit.ProductUnit>();
            foreach (var item in items) 
            {
                result.Add(ToProductUnit(item));
            }
            return result;
        }

    }
}
