using ShopProjectDataBase.DataBase.Model;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IUserTableAccess 
    {
        void Add(UserEntity item);
        void UpdateParameter(Guid id, string nameParameter, object valueParameter);
        void Delete(UserEntity item);
        IEnumerable<UserEntity> GetAll();
    }
}
