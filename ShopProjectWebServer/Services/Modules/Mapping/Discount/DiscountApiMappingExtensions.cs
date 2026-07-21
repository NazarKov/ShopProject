using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.Discount;

namespace ShopProjectWebServer.Services.Modules.Mapping.Discount
{
    public static class DiscountApiMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.Discount.Discount ToDiscount(this CreateDicountDto item)
        {
            return new ShopProjectWebServer.Models.Domain.Discount.Discount()
            {  
                Rebate = item.Discount,
                CreateAt = item.CreateAt,
                NameDiscount = item.NameDiscount,
                TotalDiscount = item.TotalDiscount,
                TypeDiscount = item.TypeDiscount,
                FinishedAt = item.FinishedAt,
                InterimAmount = item.InterimAmount, 
            };
        }
    }
}
