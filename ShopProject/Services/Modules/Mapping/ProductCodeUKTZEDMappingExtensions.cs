using ShopProject.Model.Domain.PorductCodeUKTZED;
using ShopProject.Model.UI.ProductCodeUKTZED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.MappingServise
{
    public static class ProductCodeUKTZEDMappingExtensions
    {
        public static ProductCodeUKTZEDModel ToProductCodeUKTZEDModel(this ProductCodeUKTZED item)
        {
            return new ProductCodeUKTZEDModel()
            {
                Code = item.Code,
                ID = item.ID,
                NameCode = item.NameCode,
                Status = item.Status,
            };
        }
        public static IEnumerable<ProductCodeUKTZEDModel> ToProductCodeUKTZEDModel(this IEnumerable<ProductCodeUKTZED> items)
        {
            var reslut = new List<ProductCodeUKTZEDModel>();
            foreach (var item in items) 
            {
                reslut.Add(item.ToProductCodeUKTZEDModel());
            }
            return reslut;
        }
        public static ProductCodeUKTZED ToProductCodeUKTZED(this ProductCodeUKTZEDModel item)
        {
            return new ProductCodeUKTZED()
            {
                Code = item.Code,
                Status = item.Status,
                ID = item.ID,
                NameCode = item.NameCode,
            };
        }
    }
}
