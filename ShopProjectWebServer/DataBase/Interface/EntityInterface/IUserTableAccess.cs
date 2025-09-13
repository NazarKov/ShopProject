using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;

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

        PaginatorData<UserEntity> GetAllPageColumn(double page, double countColumn, TypeStatusUser statusUser);
        PaginatorData<UserEntity> GetUsersByNamePageColumn(string name, double page, double countColumn, TypeStatusUser statusUser);
    }


}
