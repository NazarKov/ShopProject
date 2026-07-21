using ShopProjectWebServer.Api.DtoModels.Order; 
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Modules.Domain.Order.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.Order;
using OrderModel = ShopProjectWebServer.Models.Domain.Order.Order;

namespace ShopProjectWebServer.Services.Modules.Domain.Order
{
    internal class OrderService : IOrderService
    {
        private IDataBaseService _service; 

        public OrderService(IDataBaseService service)
        {
            _service = service; 
        }
        public OperationResult<bool> AddRange(IEnumerable<OrderModel> orders)
        {
            try
            {
                _service.DataBaseAccess.OrderTable.AddRange(orders.ToListOrderEntity());
                return OperationResult<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, Common.Enum.ErrorType.Server);
            } 
        }

        public IEnumerable<OrderDto> GetAll(string token)
        { 
            var result = _service.DataBaseAccess.OrderTable.GetAll();

            return result.ToOrderDto();
        }
    }
}
