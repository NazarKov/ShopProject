using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IOperationRecordersAndUserServise
    {
        public void Add(string token, Guid userID, IEnumerable<BindingUserToOperationRecorderDto> operationRecorders);
<<<<<<< HEAD
        public OperationRecorderUserDto GetOperationRecorderForUser(string token);
=======
        public IEnumerable<OperationRecorderUserDto> GetAll(string token);
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
    }
}
