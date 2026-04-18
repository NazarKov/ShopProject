using ShopProjectWebServer.Api.DtoModels.Order;

namespace ShopProjectWebServer.Services.Modules.Domain.Order
{
    public interface IOrderServise
    {
        public void AddRange(string token, IEnumerable<CreateOrderDto> orders);
        public IEnumerable<OrderDto> GetAll(string token);
    }
}
