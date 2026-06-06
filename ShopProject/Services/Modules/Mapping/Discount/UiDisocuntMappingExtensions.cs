using ShopProject.Model.Domain.Discount;
using ShopProject.Model.UI.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.Discount
{
    public static class UiDisocuntMappingExtensions
    {
        public static DiscountModel ToDiscountModel(this ShopProject.Model.Domain.Discount.Discount item)
        {
            return new DiscountModel()
            {
                TotalDiscount = item.TotalDiscount,
                ID = item.ID,
                NameDiscount = item.NameDiscount,
                TypeDiscount = item.TypeDiscount,
                CreateAt = item.CreateAt,
                FinishedAt = item.FinishedAt,
                InterimAmount = item.InterimAmount,
                Rebate = item.Rebate,
            };
        }

        public static ShopProject.Model.Domain.Discount.Discount ToDiscount(this DiscountModel item)
        {
            return new ShopProject.Model.Domain.Discount.Discount()
            {
                TotalDiscount = item.TotalDiscount,
                ID = item.ID,
                NameDiscount = item.NameDiscount,
                TypeDiscount = item.TypeDiscount,
                CreateAt = item.CreateAt,
                FinishedAt = item.FinishedAt,
                InterimAmount = item.InterimAmount,
                Rebate = item.Rebate,
            };
        }
    }
}
