using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;  

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IProductUnitTableAccess
    {
        Task<ProductUnitEntity> AddAsync(ProductUnitEntity item);
        Task UpdateAsync(ProductUnitEntity item);
        Task UpdateParameterAsync(ProductUnitEntity item, string parameter, object value);

        Task DeleteAsync(int id);
        IEnumerable<ProductUnitEntity> GetAll();
        public IEnumerable<ProductUnitEntity> GetByCode(int number, TypeStatusUnit status);
        IEnumerable<ProductUnitEntity> GetByNameAndStatus(string name, TypeStatusUnit status);
        public Task<bool> ExistsByBarCode(int code);
    }
}
