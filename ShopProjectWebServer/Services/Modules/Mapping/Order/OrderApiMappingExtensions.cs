using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.Order;
using OrderModel = ShopProjectWebServer.Models.Domain.Order.Order;

namespace ShopProjectWebServer.Services.Modules.Mapping.Order
{
    public static class OrderApiMappingExtensions
    {
        public static OrderModel ToOrder(this CreateOrderDto order)
        {
            return new OrderModel()
            {
                Operation = new ShopProjectWebServer.Models.Domain.Operation.Operation() { ID = order.OperationID },
                Product = new ShopProjectWebServer.Models.Domain.Product.Product() { ID = Guid.Parse(order.ProductID) },
                Count = order.Count,
            };
        }

        public static IEnumerable<OrderModel> ToListOrder(this IEnumerable<CreateOrderDto> order)
        {
            var result = new List<OrderModel>();

            foreach (var item in order)
            {
                result.Add(item.ToOrder());
            }
            return result;
        }

        public static OrderDto ToOrderDto(this OrderEntity order)
        {
            return new OrderDto() { Count = order.Count, OperationID = order.Operation.ID, ProductID = order.Product.ID.ToString() };
        }

        public static IEnumerable<OrderDto> ToOrderDto(this IEnumerable<OrderEntity> orders)
        {
            var result = new List<OrderDto>();
            foreach (var item in orders)
            {
                result.Add(ToOrderDto(item));
            }
            return result;
        }

    }
}
