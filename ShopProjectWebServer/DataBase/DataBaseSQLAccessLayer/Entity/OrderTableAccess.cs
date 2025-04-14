using ShopProjectDataBase.DataBase.Model;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OrderTableAccess : IOrderTableAccess<OrderEntity>
    {
        public void Add(OrderEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(OrderEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
