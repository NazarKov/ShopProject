using ShopProjectDataBase.Entities; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IMediaAccessControlTableAccess
    {
        void Add(MediaAccessControlEntity item);
        void AddRange(Guid userID, IEnumerable<MediaAccessControlEntity> items);
        void Update(MediaAccessControlEntity item);
        void Delete(MediaAccessControlEntity item);

        MediaAccessControlEntity GetLastMAC(Guid operationRecorderId);
        IEnumerable<MediaAccessControlEntity> GetAll();

        MediaAccessControlEntity GetByOperationId(int operationId);
    }
}
