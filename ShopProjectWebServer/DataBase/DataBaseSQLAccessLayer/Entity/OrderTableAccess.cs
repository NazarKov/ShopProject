using ShopProjectSQLDataBase.Context;
using ShopProjectSQLDataBase.Entities;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OrderTableAccess : IOrderTableAccess 
    {
        private string _connectionString;
        public OrderTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Add(OrderEntity item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<OrderEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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

        public void Update(OrderEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
