using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
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
        ProductCodeUKTZEDEntity GetCodeUKTZEDByCode(int number, TypeStatusCodeUKTZED statusCodeUKTZED);

        IEnumerable<ProductCodeUKTZEDEntity> GetByNameAndStatus(string name, TypeStatusCodeUKTZED status);
    } 
}
