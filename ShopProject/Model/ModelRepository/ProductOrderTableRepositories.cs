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
    class ProductOrderTableRepositories : ITableRepository<GoodsOperation,TypeParameterSetTableProductOrder>
    {

        public void Add(GoodsOperation productOrder)
        {
            using(ShopContext context = new ShopContext())
            {
                context.orders.Load();
                context.products.Load();
                context.productOrders.Load();

                GoodsOperation productOrd = new GoodsOperation();

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
