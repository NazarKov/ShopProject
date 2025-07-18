using ShopProjectDataBase.DataBase.Entities;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationRecorderTableAccess 
    {
        void Add(OperationsRecorderEntity item);
        void AddRange(IEnumerable<OperationsRecorderEntity> items);
        void AddBinding(Guid idoperationrecoreder, Guid idobjectowner);
        void Update(OperationsRecorderEntity item);
        void Delete(OperationsRecorderEntity item);
        IEnumerable<OperationsRecorderEntity> GetAll();
    }
}
