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

            foreach (var item in items)
            {
                item.Product = _contextDataBase.Products.Find(item.Product.ID);
                item.Operation = _contextDataBase.Operations.Find(item.Operation.ID);
            }
            _contextDataBase.Orders.AddRange(items);
            _contextDataBase.SaveChanges();
        }

        public void Delete(OrderEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderEntity> GetAll()
        {
            return _contextDataBase.Orders.Include(o => o.Operation).Include(p => p.Product).AsNoTracking().ToList();
        }

        public IEnumerable<OrderEntity> GetForOperation(int opearationId)
        {
            IQueryable<OrderEntity> query = _contextDataBase.Orders
                   .Include(c => c.Product)
                   .Include(o => o.Operation).Include(c => c.Product.CodeUKTZED)
                   .Include(u => u.Product.Unit)
                   .AsNoTracking(); 
            return query.Where(o => o.Operation.ID == opearationId);
        }

        public void Update(OrderEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
