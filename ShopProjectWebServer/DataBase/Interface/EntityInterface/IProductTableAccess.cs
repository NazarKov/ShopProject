using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using ShopProjectWebServer.DataBase.Helpers;
using System.Linq.Expressions;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IProductTableAccess 
    {
        Task<ProductEntity> AddAsync(ProductEntity item);
        Task AddRangeAsync(IEnumerable<ProductEntity> item);
        
        Task UpdateAsync(ProductEntity item);
        Task UpdateRangeAsync(IEnumerable<ProductEntity> items);
        Task UpdateParameterAsync(ProductEntity item , string parameter , object value);
        void Delete(ProductEntity item);

        public int GetCountStatusProduct(TypeStatusProduct status);

        IEnumerable<ProductEntity> GetAll(); 
        ProductEntity? GetByBarCode(string barCode, TypeStatusProduct statusProduct);
        public IEnumerable<ProductEntity> GetAllByBarCode(string barCode, TypeStatusProduct statusProduct);
        IEnumerable<ProductEntity> GetByNameAndStatus(string name, TypeStatusProduct status);
        public Task<bool> ExistsByBarCode(string barcode);
    }
}
