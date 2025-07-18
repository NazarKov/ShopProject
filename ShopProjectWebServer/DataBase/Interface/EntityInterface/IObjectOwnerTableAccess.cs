using ShopProjectDataBase.DataBase.Entities;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IObjectOwnerTableAccess 
    {
        void Add(ObjectOwnerEntity item);
        void AddRange(IEnumerable<ObjectOwnerEntity> item);
        void Update(ObjectOwnerEntity item);
        void Delete(ObjectOwnerEntity item);
        IEnumerable<ObjectOwnerEntity> GetAll();
    }
}
