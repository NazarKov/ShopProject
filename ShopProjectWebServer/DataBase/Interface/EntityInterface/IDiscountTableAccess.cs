namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IDiscountTableAccess<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        IEnumerable<T> GetAll();
    }
}
