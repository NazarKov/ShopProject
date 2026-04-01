using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.UI.ProductUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.MappingServise
{
    public static class ProductUnitMappingExtensions
    {
        public static ProductUnitModel ToProductUnitModel (this ProductUnit item)
        {
            return new ProductUnitModel()
            {
                ShortNameUnit = item.ShortNameUnit,
                Status = item.Status,
                ID = item.ID,
                NameUnit = item.NameUnit,
                Number = item.Number,
            };
        }

        public static IEnumerable<ProductUnitModel> ToProductUnitModel(this IEnumerable<ProductUnit> items)
        {
            var reslut = new List<ProductUnitModel>();
            foreach (var item in items)
            {
                reslut.Add(item.ToProductUnitModel());
            }
            return reslut;
        }
        public static ProductUnit ToProductUnit(this ProductUnitModel item) 
        {
            return new ProductUnit()
            {
                ID = item.ID,
                NameUnit = item.NameUnit,
                Number = item.Number,
                ShortNameUnit = item.ShortNameUnit,
                Status = item.Status, 
            };
        }
    }
}
