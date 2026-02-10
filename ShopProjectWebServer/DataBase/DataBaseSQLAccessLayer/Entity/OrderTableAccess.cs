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
        private readonly ContextDataBase _contextDataBase;
        public OrderTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public void Add(OrderEntity item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<OrderEntity> items)
        {
            _contextDataBase.Operations.Load();
            _contextDataBase.Products.Load();
            _contextDataBase.Discounts.Load();
            _contextDataBase.Orders.Load();
            if (_contextDataBase.Products != null)
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    items.ElementAt(i).Product = _contextDataBase.Products.Find(items.ElementAt(i).Product.ID);
                    items.ElementAt(i).Operation = _contextDataBase.Operations.Find(items.ElementAt(i).Operation.ID);
                }
                _contextDataBase.Orders.AddRange(items);
            }
            _contextDataBase.SaveChanges();
        }

        public void Delete(OrderEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderEntity> GetAll()
        {
            _contextDataBase.Operations.Load();
            _contextDataBase.Products.Load();
            _contextDataBase.Discounts.Load();
            _contextDataBase.Orders.Load();

            if (_contextDataBase.Operations.Count() != 0)
            {
                return _contextDataBase.Orders.ToList();
            }
            else
            {
                return new List<OrderEntity>();
            }
        }

        public IEnumerable<OrderEntity> GetForOperation(int opearationId)
        {
            IQueryable<OrderEntity> query = _contextDataBase.Orders
                   .Include(c => c.Product)
                   .Include(o => o.Operation).Include(c => c.Product.CodeUKTZED)
                   .Include(u => u.Product.Unit)
                   .AsNoTracking();

            query = query.Where(o => o.Operation.ID == opearationId);

            var result = query.ToList();
            return result;
        }

        public void Update(OrderEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
