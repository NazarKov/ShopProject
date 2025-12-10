using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities; 
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class DiscountTableAccess : IDiscountTableAccess
    {
        private DbContextOptions<ContextDataBase> _option;
        public DiscountTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }
        public int Add(DiscountEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                { 
                    context.Discounts.Load();
                    if (context.Discounts != null)
                    {  
                        context.Discounts.Add(item);
                    }
                    context.SaveChanges(); 
                }
            }
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
