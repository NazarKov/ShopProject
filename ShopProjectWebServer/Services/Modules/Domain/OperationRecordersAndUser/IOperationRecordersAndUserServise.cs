using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;

namespace ShopProjectWebServer.Services.Modules.Domain.OperationRecordersAndUser
{
    public interface IOperationRecordersAndUserServise
    {
        public void Add(string token, Guid userID, IEnumerable<BindingUserToOperationRecorderDto> operationRecorders); 
        public OperationRecorderUserDto GetOperationRecorderForUser(string token); 
    }
}
