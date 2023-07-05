using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.ModelRepository
{
    public enum TypeParameterSetTableProductOrder
    {

    }
    class ProductOrderTableRepositories : ITableRepository<ProductOrder,TypeParameterSetTableProductOrder>
    {

        public void Add(ProductOrder productOrder)
        {
            using(ShopContext context = new ShopContext())
            {
                context.orders.Load();
                context.products.Load();
                context.productOrders.Load();

                ProductOrder productOrd = new ProductOrder();

                productOrd.Order = context.orders.Find(productOrder.Order.ID);
                productOrd.Product = context.products.Find(productOrder.Product.ID);
                productOrd.count = productOrder.count;

                context.products.Find(productOrder.Product.ID).count -= productOrder.count;

                context.productOrders.Add(productOrd);

                context.SaveChanges();
            }
        }
        public IEnumerable<object> GetAll()
        {
            using(ShopContext context = new ShopContext())
            {
                context.productOrders.Load();
                if(context.products!=null)
                {
                    return context.products;
                }
                return Enumerable.Empty<object>();
            }
        }
    }
}
