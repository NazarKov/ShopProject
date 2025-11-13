using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IProductTableAccess 
    {
        void Add(ProductEntity item);
        Task AddRangeAsync(IEnumerable<ProductEntity> item);
        
        void Update(ProductEntity item);
        void UpdateRange(IEnumerable<ProductEntity> items);
        void UpdateParameter(ProductEntity item , string parameter , object value);
        void Delete(ProductEntity item); 
        IEnumerable<ProductEntity> GetAll();
        ProductEntity GetByBarCode(string barCode);
        ProductEntity GetByBarCode(string barCode, TypeStatusProduct statusProduct);

        IEnumerable<ProductEntity> GetByNameAndStatu(string name, TypeStatusProduct status);

    }
}
