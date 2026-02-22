using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Discount;
using ShopProject.UIModel.StoragePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class DisocuntMappingExtensions
    {
        public static CreateDiscountDto ToCreateDicount(this Discount item)
        {
            return new CreateDiscountDto()
            {
                CreateAt = item.CreateAt,
                Discount = item.Rebate,
                TotalDiscount = item.TotalDiscount,
                NameDiscount = item.NameDiscount,
                TypeDiscount = item.TypeDiscount,
                FinishedAt = item.FinishedAt,
                InterimAmount = item.InterimAmount,
            };
        }
        public static Discount ToDicount(this DiscountDto item)
        {
            return new Discount()
            {
                CreateAt = item.CreateAt,
                Rebate = item.Discount,
                TotalDiscount = item.TotalDiscount,
                NameDiscount = item.NameDiscount,
                TypeDiscount = item.TypeDiscount,
                FinishedAt = item.FinishedAt,
                InterimAmount = item.InterimAmount,
            };
        }
    }
}
