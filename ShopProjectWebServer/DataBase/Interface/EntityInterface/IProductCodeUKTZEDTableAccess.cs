using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IProductCodeUKTZEDTableAccess
    {
        public Task<ProductCodeUKTZEDEntity> AddAsync(ProductCodeUKTZEDEntity item);
        public Task UpdateAsync(ProductCodeUKTZEDEntity item);
        public Task UpdateParameterAsync(ProductCodeUKTZEDEntity item, string parameter, object value);

        public void Delete(ProductCodeUKTZEDEntity item);
        public IEnumerable<ProductCodeUKTZEDEntity> GetAll();
        public IEnumerable<ProductCodeUKTZEDEntity> GetByCode(int number, TypeStatusCodeUKTZED statusCodeUKTZED);
        public IEnumerable<ProductCodeUKTZEDEntity> GetByNameAndStatus(string name, TypeStatusCodeUKTZED status); 
        public Task<bool> ExistsByCodeAsync(string code);
    } 
}
