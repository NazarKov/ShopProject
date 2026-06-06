using ShopProject.Model.Domain.Discount;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.Discount
{
    public static class ApiDisocuntMappingExtensions
    {
        public static CreateDiscountDto ToCreateDicount(this ShopProject.Model.Domain.Discount.Discount item)
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
        public static ShopProject.Model.Domain.Discount.Discount ToDicount(this DiscountDto item)
        {
            return new ShopProject.Model.Domain.Discount.Discount()
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
