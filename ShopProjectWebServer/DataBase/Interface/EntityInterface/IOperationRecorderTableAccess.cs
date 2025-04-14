namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationRecorderTableAccess<T>
    {
        void Add(T item);
        void AddRange(IEnumerable<T> items);
        void AddBinding(Guid idoperationrecoreder, Guid idobjectowner);
        void Update(T item);
        void Delete(T item);
        IEnumerable<T> GetAll();
    }
}
