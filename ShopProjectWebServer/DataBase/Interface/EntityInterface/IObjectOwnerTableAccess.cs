using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IObjectOwnerTableAccess
    {
        void Add(ObjectOwnerEntity item);
        void AddRange(IEnumerable<ObjectOwnerEntity> item);
        void Update(ObjectOwnerEntity item);
        void Delete(ObjectOwnerEntity item);
        IEnumerable<ObjectOwnerEntity> GetAll();
        IEnumerable<ObjectOwnerEntity> GetByNameAndStatus(string name, TypeStatusObjectOwner status);  
    }
}
