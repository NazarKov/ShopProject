namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationRecorederUserTableAccess<T>
    {
        void Add(T item);
        void AddRange(IEnumerable<T> item);
        void Update(T item);
        void Delete(T item);
        IEnumerable<T> GetAll();
    }
}
