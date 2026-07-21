using ShopProjectWebServer.Api.DtoModels.Order;
using ShopProjectWebServer.Services.Common;
using OrderModel = ShopProjectWebServer.Models.Domain.Order.Order;

namespace ShopProjectWebServer.Services.Modules.Domain.Order.Interface
{
    public interface IOrderService
    {
        public OperationResult<bool> AddRange(IEnumerable<OrderModel> orders);
        public IEnumerable<OrderDto> GetAll(string token);
    }
}
