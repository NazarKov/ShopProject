
namespace ShopProjectWebServer.Models.Domain.Order
{
    public class Order
    {
        public int ID { get; set; } 
        public int Count { get; set; } = 0; 
        public ShopProjectWebServer.Models.Domain.Product.Product? Product { get; set; } 
        public ShopProjectWebServer.Models.Domain.Operation.Operation? Operation { get; set; }
    }
}
