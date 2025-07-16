using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.Api.Helpers.ProductContoller;
using ShopProjectWebServer.DataBase.Helpers;

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

        ProductInfo GetProductInfo();
        IEnumerable<T> GetAll();
        T GetByBarCode(string barCode, TypeStatusProduct statusProduct);
        PaginatorData<T> GetAllPageColumn(double page, double countColumn,TypeStatusProduct statusProduct);
        PaginatorData<T> GetProductByNamePageColumn(string name, double page, double countColumn, TypeStatusProduct statusProduct);
    }
}
