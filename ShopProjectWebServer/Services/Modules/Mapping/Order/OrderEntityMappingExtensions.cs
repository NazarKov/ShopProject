using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.Order;
using OrderModel = ShopProjectWebServer.Models.Domain.Order.Order;

namespace ShopProjectWebServer.Services.Modules.Mapping.Order
{
    public static class OrderEntityMappingExtensions
    {
        public static OrderEntity ToOrderEntiti(this OrderModel order)
        {
            return new OrderEntity()
            {
                Operation = new OperationEntity() { ID = order.Operation.ID },
                Product = new ProductEntity() { ID = order.Product.ID },
                Count = order.Count,
            };
        }

        public static IEnumerable<OrderEntity> ToListOrderEntity(this IEnumerable<OrderModel> order)
        {
            var result = new List<OrderEntity>();

            foreach (var item in order)
            {
                result.Add(item.ToOrderEntiti());
            }
            return result;
        }
    }
}
