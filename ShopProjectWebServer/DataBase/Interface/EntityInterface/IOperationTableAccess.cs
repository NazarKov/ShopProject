namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationTableAccess<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        IEnumerable<T> GetAll();
        T GetLastItem();
    }
}
