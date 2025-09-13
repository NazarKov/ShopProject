using ShopProjectDataBase.Entities; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationRecorederUserTableAccess
    {
        void Add(OperationsRecorderUserEntity item);
        void AddRange(Guid userID , IEnumerable<OperationsRecorderEntity> items);
        void Update(OperationsRecorderUserEntity item);
        void Delete(OperationsRecorderUserEntity item);
        IEnumerable<OperationsRecorderUserEntity> GetAll();
    }
}
