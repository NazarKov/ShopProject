using ShopProjectDataBase.Entities;

namespace ShopProjectWebServer.Services.Modules.Mapping.Discount
{
    public static class DiscountEntityMappingExtensions
    {
        public static DiscountEntity ToDiscountEntity(this ShopProjectWebServer.Models.Domain.Discount.Discount item)
        {
            return new DiscountEntity()
            {
                ID = item.ID,
                Discount = item.Rebate,
                CreateAt = item.CreateAt,
                NameDiscount = item.NameDiscount,
                TotalDiscount = item.TotalDiscount,
                TypeDiscount = item.TypeDiscount,
                FinishedAt = item.FinishedAt,
                InterimAmount = item.InterimAmount,
            };
        }
        public static ShopProjectWebServer.Models.Domain.Discount.Discount ToDiscount(this DiscountEntity item)
        {
            return new ShopProjectWebServer.Models.Domain.Discount.Discount()
            {
                ID = item.ID,
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
