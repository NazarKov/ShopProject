using ShopProjectDataBase.DataBase.Entities;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationRecorederUserTableAccess
    {
        void Add(OperationsRecorderUserEntity item);
        void AddRange(IEnumerable<OperationsRecorderUserEntity> item);
        void Update(OperationsRecorderUserEntity item);
        void Delete(OperationsRecorderUserEntity item);
        IEnumerable<OperationsRecorderUserEntity> GetAll();
    }
}
