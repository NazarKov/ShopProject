using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IProductCodeUKTZEDTableAccess
    {
        void Add(ProductCodeUKTZEDEntity item);
        void Update(ProductCodeUKTZEDEntity item);
        void UpdateParameter(ProductCodeUKTZEDEntity item, string parameter, object value);

        void Delete(ProductCodeUKTZEDEntity item);
        IEnumerable<ProductCodeUKTZEDEntity> GetAll();
        PaginatorData<ProductCodeUKTZEDEntity> GetAllPageColumn(double page, double countColumn, TypeStatusCodeUKTZED statusCodeUKTZED);
        ProductCodeUKTZEDEntity GetCodeUKTZEDByCode(int number, TypeStatusCodeUKTZED statusCodeUKTZED);
        PaginatorData<ProductCodeUKTZEDEntity> GetCodeUKTZEDByNamePageColumn(string name, double page, double countColumn, TypeStatusCodeUKTZED statusCodeUKTZED);
    } 
}
