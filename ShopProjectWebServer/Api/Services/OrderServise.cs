using ShopProjectWebServer.Api.DtoModels.Order;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class OrderServise : IOrderServise
    {
        public void AddRange(string token, IEnumerable<CreateOrderDto> orders)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.OrderTable.AddRange(orders.ToListOrderEntity());
        }

        public IEnumerable<OrderDto> GetAll(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var result = DataBaseMainController.DataBaseAccess.OrderTable.GetAll();

            return result.ToOrderDto();
        }
    }
}
