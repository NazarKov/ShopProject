using ShopProjectWebServer.Api.DtoModels.Discount;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class DiscountServise : IDiscountServise
    {
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;
        public DiscountServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
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
