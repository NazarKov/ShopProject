using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IOperationRecordersAndUserServise
    {
        public void Add(string token, Guid userID, IEnumerable<BindingUserToOperationRecorderDto> operationRecorders); 
        public OperationRecorderUserDto GetOperationRecorderForUser(string token); 
    }
}
