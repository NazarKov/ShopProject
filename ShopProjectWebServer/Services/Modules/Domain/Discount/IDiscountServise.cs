using ShopProjectWebServer.Api.DtoModels.Discount;

namespace ShopProjectWebServer.Services.Modules.Domain.Discount
{
    public interface IDiscountServise
    {
        public int Add(string token, CreateDicountDto createDicountDto);
        public void Get(string token, DiscountDto discountDto);
    }
}
