using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.Api.Helpers.ProductContoller;
using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IProductTableAccess 
    {
        void Add(ProductEntity item);
        void AddRange(IEnumerable<ProductEntity> item);
        
        void Update(ProductEntity item);
        void UpdateRange(IEnumerable<ProductEntity> items);
        void UpdateParameter(ProductEntity item , string parameter , object value);
        void Delete(ProductEntity item);

        ProductInfo GetProductInfo();
        IEnumerable<ProductEntity> GetAll();
        ProductEntity GetByBarCode(string barCode, TypeStatusProduct statusProduct);
        PaginatorData<ProductEntity> GetAllPageColumn(double page, double countColumn,TypeStatusProduct statusProduct);
        PaginatorData<ProductEntity> GetProductByNamePageColumn(string name, double page, double countColumn, TypeStatusProduct statusProduct);
    }
}
