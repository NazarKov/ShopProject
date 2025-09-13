using ShopProjectDataBase.Entities; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IUserRoleTableAccess 
    {
        void Add(UserRoleEntity item);
        void Update(UserRoleEntity item);
        void Delete(UserRoleEntity item);
        IEnumerable<UserRoleEntity> GetAll();
    }
}
