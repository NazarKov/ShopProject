using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities; 
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class DiscountTableAccess : IDiscountTableAccess
    {
        private readonly ContextDataBase _contextDataBase;
        public DiscountTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }
        public int Add(DiscountEntity item)
        {
            _contextDataBase.Discounts.Load();
            if (_contextDataBase.Discounts != null)
            {
                _contextDataBase.Discounts.Add(item);
            }
            _contextDataBase.SaveChanges();
            return item.ID;
        }

        public void Delete(DiscountEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DiscountEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(DiscountEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
