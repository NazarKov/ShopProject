using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationRecorderTableAccess
    {
        public Task<OperationsRecorderEntity> AddAsync(OperationsRecorderEntity item);
        public Task<IEnumerable<OperationsRecorderEntity>> AddRangeAsync(IEnumerable<OperationsRecorderEntity> items); 

 

        void AddBinding(Guid idoperationrecoreder, Guid idobjectowner);
        void Update(OperationsRecorderEntity item);
        void Delete(OperationsRecorderEntity item);
        IEnumerable<OperationsRecorderEntity> GetAll();
        //IEnumerable<OperationsRecorderEntity> SearchByNameAndUser(string item, Guid userId);
        //IEnumerable<OperationsRecorderEntity> SearchByNumberAndUser(string item, Guid userId);
        IEnumerable<OperationsRecorderEntity> GetByNameAndStatus(string name, TypeStatusOperationRecorder status);

        public Task<bool> ExistsByName(string name);
    }
} 
