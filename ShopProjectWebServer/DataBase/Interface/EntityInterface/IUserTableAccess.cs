namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IUserTableAccess<T>
    {
        void Add(T item);
        void UpdateParameter(Guid id, string nameParameter, object valueParameter);
        void Delete(T item);
        IEnumerable<T> GetAll();
    }
}
