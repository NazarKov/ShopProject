using ShopProject.Model.UI.ProductCodeUKTZED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.ProductCodeUKTZED
{
    public static class UiProductCodeUKTZEDMappingExtensions
    {
        public static ProductCodeUKTZEDModel ToProductCodeUKTZEDModel(this ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED item)
        {
            return new ProductCodeUKTZEDModel()
            {
                Code = item.Code,
                ID = item.ID,
                NameCode = item.NameCode,
                Status = item.Status,
            };
        }
        public static IEnumerable<ProductCodeUKTZEDModel> ToProductCodeUKTZEDModel(this IEnumerable<ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED> items)
        {
            var reslut = new List<ProductCodeUKTZEDModel>();
            foreach (var item in items) 
            {
                reslut.Add(item.ToProductCodeUKTZEDModel());
            }
            return reslut;
        }
        public static ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED ToProductCodeUKTZED(this ProductCodeUKTZEDModel item)
        {
            return new ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED()
            {
                Code = item.Code,
                Status = item.Status,
                ID = item.ID,
                NameCode = item.NameCode,
            };
        }
    }
}
