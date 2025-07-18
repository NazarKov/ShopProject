using ShopProjectDataBase.DataBase.Entities;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IDiscountTableAccess
    {
        void Add(DiscountEntity item);
        void Update(DiscountEntity item);
        void Delete(DiscountEntity item);
        IEnumerable<DiscountEntity> GetAll();
    }
}
