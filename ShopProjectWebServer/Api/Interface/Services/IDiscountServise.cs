using ShopProjectWebServer.Api.DtoModels.Discount;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IDiscountServise
    {
        public int Add(string token, CreateDicountDto createDicountDto);
        public void Get(string token, DiscountDto discountDto);
    }
}
