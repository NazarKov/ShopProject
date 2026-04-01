using ShopProject.Model.Domain.Order;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping
{
    public static class OrderMappingExtensions
    {
        public static IEnumerable<CreateOrderDto> ToListCreatOrderDto(this List<Order> orders)
        {
            var result = new List<CreateOrderDto>();
            foreach (var order in orders) 
            {
                if(order.Product != null && order.Operation != null)
                {
                    result.Add(new CreateOrderDto()
                    {
                        ProductID = order.Product.ID.ToString(),
                        OperationID = order.Operation.ID,
                        Count = order.Count,
                    });
                }
            }
            return result;
        }
    }
}
