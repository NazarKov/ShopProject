namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IProductTableAccess<T>
    {
        void Add(T item);
        void AddRange(IEnumerable<T> item);
        
        void Update(T item);
        void UpdateRange(IEnumerable<T> items);
        void UpdateParameter(T item , string parameter , object value);
        void Delete(T item);

        T GetByBarCode(string barCode);
        IEnumerable<T> GetAll();
    }
}
