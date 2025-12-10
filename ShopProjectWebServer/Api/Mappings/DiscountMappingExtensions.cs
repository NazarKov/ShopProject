using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.Discount;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class DiscountMappingExtensions
    {
        public static DiscountEntity ToDiscount (this CreateDicountDto item)
        {
            return new DiscountEntity()
            {
                CreateAt = item.CreateAt,
                Discount = item.Discount,
                FinishedAt = item.FinishedAt,
                InterimAmount = item.InterimAmount,
                NameDiscount = item.NameDiscount,
                TotalDiscount = item.TotalDiscount,
                TypeDiscount = item.TypeDiscount,
            };
        }
    }
}
