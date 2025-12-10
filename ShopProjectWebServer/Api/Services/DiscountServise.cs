using ShopProjectWebServer.Api.DtoModels.Discount;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class DiscountServise : IDiscountServise
    {
        public int Add(string token, CreateDicountDto createDicountDto)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.DiscountTable.Add(createDicountDto.ToDiscount());
        }

        public void Get(string token, DiscountDto discountDto)
        {
            throw new NotImplementedException();
        }
    }
}
