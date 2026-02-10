using ShopProjectWebServer.Api.DtoModels.Order;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class OrderServise : IOrderServise
    {
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public OrderServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
        }
        public void AddRange(string token, IEnumerable<CreateOrderDto> orders)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.OrderTable.AddRange(orders.ToListOrderEntity());
        }

        public IEnumerable<OrderDto> GetAll(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var result = _controller.DataBaseAccess.OrderTable.GetAll();

            return result.ToOrderDto();
        }
    }
}
