using ShopProjectWebServer.Api.DtoModels.Discount;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Modules.Authorization;

namespace ShopProjectWebServer.Services.Modules.Domain.Discount
{
    internal class DiscountService : IDiscountServise
    {
        private DataBaseService _controller;
        private AuthorizationService _authorizationServise;
        public DiscountService(DataBaseService controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationService(controller);
        }
        public int Add(string token, CreateDicountDto createDicountDto)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _controller.DataBaseAccess.DiscountTable.Add(createDicountDto.ToDiscount());
        }

        public void Get(string token, DiscountDto discountDto)
        {
            throw new NotImplementedException();
        }
    }
}
