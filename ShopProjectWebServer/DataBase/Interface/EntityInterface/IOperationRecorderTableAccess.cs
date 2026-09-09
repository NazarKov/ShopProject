using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationRecorderTableAccess
    {
        public Task<OperationsRecorderEntity> AddAsync(OperationsRecorderEntity item);
        public Task<IEnumerable<OperationsRecorderEntity>> AddRangeAsync(IEnumerable<OperationsRecorderEntity> items); 
        public Task<OperationsRecorderEntity> Update(OperationsRecorderEntity item);
        public Task UpdateParameterAsync(Guid id, string nameParameter, object valueParameter);
        void AddBinding(Guid idoperationrecoreder, Guid idobjectowner);
        void Delete(OperationsRecorderEntity item);
        IEnumerable<OperationsRecorderEntity> GetAll(); 
        IEnumerable<OperationsRecorderEntity> GetByNameAndStatus(string name, TypeStatusOperationRecorder status);

        public Task<bool> ExistsByName(string name);
    }
} 
