using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OrderTableAccess : IOrderTableAccess
    {
        private DbContextOptions<ContextDataBase> _option;
        public OrderTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }

        public void Add(OrderEntity item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<OrderEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();
                    context.Products.Load();
                    context.Discounts.Load();
                    context.Orders.Load();
                    if (context.Products != null)
                    {
                        for (int i = 0; i < items.Count(); i++)
                        {
                            items.ElementAt(i).Product = context.Products.Find(items.ElementAt(i).Product.ID);
                            items.ElementAt(i).Operation = context.Operations.Find(items.ElementAt(i).Operation.ID);
                        }
                        context.Orders.AddRange(items);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void Delete(OrderEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();
                    context.Products.Load();
                    context.Discounts.Load();
                    context.Orders.Load();

                    if (context.Operations.Count() != 0)
                    {
                        return context.Orders.ToList();
                    }
                    else
                    {
                        return new List<OrderEntity>();
                    }
                }
                return null;
            }
        }

        public IEnumerable<OrderEntity> GetForOperation(int opearationId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                IQueryable<OrderEntity> query = context.Orders
                    .Include(c => c.Product)
                    .Include(o => o.Operation).Include(c => c.Product.CodeUKTZED)
                    .Include(u => u.Product.Unit)
                    .AsNoTracking();

                query = query.Where(o => o.Operation.ID == opearationId);

                var result = query.ToList();
                return result;
            }
        }

        public void Update(OrderEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
