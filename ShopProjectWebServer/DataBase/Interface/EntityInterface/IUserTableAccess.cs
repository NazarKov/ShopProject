using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IUserTableAccess
    {
        void Add(UserEntity item);
        void Update(UserEntity item);
        void UpdateParameter(Guid id, string nameParameter, object valueParameter);
        void Delete(UserEntity item);
        IEnumerable<UserEntity> GetAll();
        UserEntity GetUser(string token); 
        UserEntity Authorization(string login, string password);
        IEnumerable<UserEntity> GetByNameAndStatus(string name, TypeStatusUser status); 
        UserEntity GetById(Guid id);
    }


}
