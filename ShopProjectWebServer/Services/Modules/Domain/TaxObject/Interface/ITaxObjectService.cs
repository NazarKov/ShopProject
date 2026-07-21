using ShopProjectWebServer.Services.Common;
using TaxObjectModel = ShopProjectWebServer.Models.Domain.TaxObject.TaxObject;
namespace ShopProjectWebServer.Services.Modules.Domain.TaxObject.Interface
{
    public interface ITaxObjectService
    {
        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int>> GetByNamePageColumn(string name,
         ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int> paginator);
        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int>> GetPageColumn(
            ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int> paginator);

        public Task<OperationResult<TaxObjectModel>> Add(TaxObjectModel taxObject);
        public Task<OperationResult<IEnumerable<TaxObjectModel>>> AddRange(IEnumerable<TaxObjectModel> taxObjects);

        public Task<OperationResult<bool>> AddBindingOpearationRecorderToTaxObject(Guid idTaxObject, IEnumerable<ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder> operationRecorders);
        public Task<OperationResult<bool>> AddBindingUserToTaxObject(Guid idTaxObject, IEnumerable<ShopProjectWebServer.Models.Domain.User.User> users);

        public OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.TaxObjectUser.TaxObjectUser>> GetTaxObjectsAssignedUser(Guid userId);

        //public bool AddRange(string token, IEnumerable<CreateObjectOwnerDto> ObjectOwners);
        //public bool Delete(string token, string id);

        //public IEnumerable<ObjectOwnerListDto> GetAll(string token);

    }
}
