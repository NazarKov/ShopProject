using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IProductUnitTableAccess
    {
        void Add(ProductUnitEntity item);
        void Update(ProductUnitEntity item);
        void UpdateParameter(ProductUnitEntity item, string parameter, object value);

        void Delete(ProductUnitEntity item);
        IEnumerable<ProductUnitEntity> GetAll();
        ProductUnitEntity GetUnitByCode(int number, TypeStatusUnit status);

        IEnumerable<ProductUnitEntity> GetByNameAndStatus(string name, TypeStatusUnit status);
    }
}
