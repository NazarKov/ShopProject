using ShopProjectDataBase.Entities; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOrderTableAccess
    {
        void Add(OrderEntity item);
        void AddRange(IEnumerable<OrderEntity> items);
        void Update(OrderEntity item);
        void Delete(OrderEntity item);
        IEnumerable<OrderEntity> GetAll();
        IEnumerable<OrderEntity> GetForOperation(int opearationId);
    }
}
