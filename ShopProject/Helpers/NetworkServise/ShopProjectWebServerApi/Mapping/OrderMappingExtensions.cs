using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Order;
using ShopProject.UIModel.SalePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class OrderMappingExtensions
    {
        public static IEnumerable<CreateOrderDto> ToListCreatOrderDto(this List<Order> orders)
        {
            var result = new List<CreateOrderDto>();
            foreach (var order in orders) 
            {
                result.Add(new CreateOrderDto()
                {
                    ProductID = order.Product.ID.ToString(),
                    OperationID = order.Operation.ID,
                    Count = order.Count,
                });
            }
            return result;
        }
    }
}
