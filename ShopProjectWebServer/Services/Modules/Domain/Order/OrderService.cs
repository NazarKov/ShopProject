using ShopProjectWebServer.Api.DtoModels.Order;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Modules.Authorization;

namespace ShopProjectWebServer.Services.Modules.Domain.Order
{
    internal class OrderService : IOrderServise
    {
        private DataBaseService _controller;
        private AuthorizationService _authorizationServise;

        public OrderService(DataBaseService controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationService(controller);
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
