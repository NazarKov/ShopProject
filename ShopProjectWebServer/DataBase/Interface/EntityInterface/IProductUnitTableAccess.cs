using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IProductUnitTableAccess<T>
    {
        void Add(T item);
        void Update(T item);
        void UpdateParameter(T item, string parameter, object value);
        
        void Delete(T item);
        IEnumerable<T> GetAll();
        PaginatorData<T> GetAllPageColumn(double page, double countColumn, TypeStatusUnit statusUnit);
        T GetUnitByCode(int number, TypeStatusUnit statusProduct);
        PaginatorData<T> GetUnitByNamePageColumn(string name, double page, double countColumn, TypeStatusUnit statusUnit);
    }
}
