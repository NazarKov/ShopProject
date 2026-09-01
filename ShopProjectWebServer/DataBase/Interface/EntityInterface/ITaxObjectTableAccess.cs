using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface ITaxObjectTableAccess
    {
        public Task<TaxObjectEntity> AddAsync(TaxObjectEntity item);
        public Task<IEnumerable<TaxObjectEntity>> AddRangeAsync(IEnumerable<TaxObjectEntity> items);
        public Task UpdateAsync(TaxObjectEntity item);
        void Delete(TaxObjectEntity item);
        IEnumerable<TaxObjectEntity> GetAll();
        IEnumerable<TaxObjectEntity> GetByNameAndStatus(string name, TypeStatusTaxObject status);
        public Task<bool> ExistsByName(string name);
        public Task AddBindingOperationRecorderToTaxObject(Guid idTaxObject, IEnumerable<OperationsRecorderEntity> operationsRecorders);
        public Task AddBindingUserToTaxObject(Guid idTaxObject, IEnumerable<UserEntity> users);
        public IEnumerable<TaxObjectUserEnitity> GetTaxObjectsAssignedUser(Guid userID);
        public Task UpdateParameterAsync(Guid id, string nameParameter, object valueParameter);
    }
}
