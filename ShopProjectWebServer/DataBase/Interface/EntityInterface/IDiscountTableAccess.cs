using ShopProjectDataBase.Entities; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IDiscountTableAccess
    {
        int Add(DiscountEntity item);
        void Update(DiscountEntity item);
        void Delete(DiscountEntity item);
        IEnumerable<DiscountEntity> GetAll();
    }
}
