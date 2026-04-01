using ShopProject.Model.UI.Operation;
using ShopProject.Model.UI.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.Order
{
    internal class OrderModel
    {
        public int ID { get; set; }
        public int Count { get; set; } = 0;
        public ProductModel? Product { get; set; }
        public OperationModel? Operation { get; set; }
    }
}
