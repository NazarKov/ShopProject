using ShopProjectDataBase.Entities; 
using ShopProjectWebServer.Api.DtoModels.Order;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class OrderMappingExtensions
    {
        public static OrderEntity ToOrderEntiti(this CreateOrderDto order)
        {
            return new OrderEntity() {
                Operation = new OperationEntity() { ID = order.OperationID },
                Product = new ProductEntity() { ID = order.ProductID},
                Count = order.Count,
            };
        }

        public static List<OrderEntity> ToListOrderEntity(this List<CreateOrderDto> order) 
        {
            var result = new List<OrderEntity>();

            foreach (var item in order) 
            {
                result.Add(new OrderEntity()
                {
                    Operation = new OperationEntity() { ID = item.OperationID },
                    Product = new ProductEntity() { ID = item.ProductID },
                    Count = item.Count,
                });
            }
            return result;
        }

    }
}
