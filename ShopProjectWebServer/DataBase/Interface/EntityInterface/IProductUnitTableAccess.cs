using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
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
        PaginatorData<ProductUnitEntity> GetAllPageColumn(double page, double countColumn, TypeStatusUnit statusUnit);
        ProductUnitEntity GetUnitByCode(int number, TypeStatusUnit statusProduct);
        PaginatorData<ProductUnitEntity> GetUnitByNamePageColumn(string name, double page, double countColumn, TypeStatusUnit statusUnit);
    }
}
