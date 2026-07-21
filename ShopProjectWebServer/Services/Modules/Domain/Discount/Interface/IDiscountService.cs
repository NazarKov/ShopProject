using ShopProjectWebServer.Api.DtoModels.Discount;
using ShopProjectWebServer.Services.Common;
using DiscountModel = ShopProjectWebServer.Models.Domain.Discount.Discount;

namespace ShopProjectWebServer.Services.Modules.Domain.Discount.Interface
{
    public interface IDiscountService
    {
        public OperationResult<int> Add(DiscountModel discount);
        public void Get(string token, DiscountDto discountDto);
    }
}
