using ShopProject.Model.Domain.Discount;
using ShopProject.Model.UI.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.MappingServise
{
    internal static class DisocuntMappingExtensions
    {
        public static DiscountModel ToDiscountModel(this Discount item)
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

        public static Discount ToDiscount(this DiscountModel item)
        {
            return new Discount()
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
