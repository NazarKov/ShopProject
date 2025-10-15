using ShopProjectWebServer.Api.DtoModels.Order;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IOrderServise
    {
        public void AddRange(string token, IEnumerable<CreateOrderDto> orders);
        public IEnumerable<OrderDto> GetAll(string token);
    }
}
