using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ProductCodeUKTZED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.ProductCodeUKTZED
{
    public static class ApiProductCodeUKTZEDMappingExtensions
    {
        public static CreateProductUKTZEDDto ToProductCodeUKTZED(this ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED item)
        {
            return new CreateProductUKTZEDDto()
            {
                Code = item.Code,
                Status = (int)item.Status,
                NameCode = item.NameCode,
            };
        }
        public static ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED ToProductCodeUKTZED(this ProductCodeUKTZEDDto item)
        {
            return new ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED()
            {
                ID = item.ID,
                Code = item.Code,
                Status = (TypeStatusCodeUKTZED)item.Status,
                NameCode = item.NameCode,
            };
        }

        public static IEnumerable<ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED> ToProductCodeUKTZED(this  IEnumerable<ProductCodeUKTZEDDto> items)
        {
            var result = new List<ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED>();
            foreach (var item in items) 
            {
                result.Add(ToProductCodeUKTZED(item));
            }
            return result;
        }
        public static UpdateProductCodeUKTZEDDto ToUpdateProductCodeUKTZEDDto(this ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED item)
        {
            return new UpdateProductCodeUKTZEDDto()
            {
                ID = (int)item.ID,
                Code = item.Code,
                Status = (int)item.Status,
                NameCode = item.NameCode,
            };
        }
        public static CreateProductUKTZEDDto ToCreateProductCodeUKTZEDDto(this ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED item)
        {
            return new CreateProductUKTZEDDto()
            { 
                Code = item.Code,
                Status = (int)item.Status,
                NameCode = item.NameCode, 
            };
        }
    }
}
