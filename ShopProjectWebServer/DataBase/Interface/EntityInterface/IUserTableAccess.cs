using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IUserTableAccess
    {
        public Task<UserEntity> AddAsync(UserEntity item);
        public Task UpdateAsync(UserEntity item);
        public Task UpdateParameterAsync(Guid id, string nameParameter, object valueParameter);
        public Task DeleteAsync(Guid id);
        public IEnumerable<UserEntity> GetAll();
        public UserEntity? GetUser(string token);
        public UserEntity? GetByLogin(string login);
        public IEnumerable<UserEntity> GetByNameAndStatus(string name, TypeStatusUser status); 
        public UserEntity? GetById(Guid id);
        public Task<bool> ExistsByLogin(string login);
    }


}
