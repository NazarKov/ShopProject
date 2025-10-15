using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IOperationRecordersAndUserServise
    {
        public void Add(string token, Guid userID, IEnumerable<BindingUserToOperationRecorderDto> operationRecorders);
        public IEnumerable<OperationRecorderUserDto> GetAll(string token);
    }
}
