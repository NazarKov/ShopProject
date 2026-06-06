using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.UI.ProductUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.ProductUnit
{
    public static class UiProductUnitMappingExtensions
    {
        public static ProductUnitModel ToProductUnitModel (this ShopProject.Model.Domain.ProductUnit.ProductUnit item)
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

        public static IEnumerable<ProductUnitModel> ToProductUnitModel(this IEnumerable<ShopProject.Model.Domain.ProductUnit.ProductUnit> items)
        {
            var reslut = new List<ProductUnitModel>();
            foreach (var item in items)
            {
                reslut.Add(item.ToProductUnitModel());
            }
            return reslut;
        }
        public static ShopProject.Model.Domain.ProductUnit.ProductUnit ToProductUnit(this ProductUnitModel item) 
        {
            return new ShopProject.Model.Domain.ProductUnit.ProductUnit()
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
