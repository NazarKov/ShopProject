using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.Product; 

namespace ShopProject.Model.Domain.Order
{
    public class Order
    {
        public int ID { get; set; } 
        public int Count { get; set; } = 0; 
        public Product.Product? Product { get; set; } 
        public Operation.Operation? Operation { get; set; }
    }
}
