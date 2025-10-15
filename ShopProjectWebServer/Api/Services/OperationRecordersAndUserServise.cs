using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class OperationRecordersAndUserServise : IOperationRecordersAndUserServise
    {
        public void Add(string token, Guid userID, IEnumerable<BindingUserToOperationRecorderDto> operationRecorders)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            DataBaseMainController.DataBaseAccess.OperationRecorederUserTable.AddRange(userID, operationRecorders.ToOperationRecordersEntity());
        }

        public IEnumerable<OperationRecorderUserDto> GetAll(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var result = DataBaseMainController.DataBaseAccess.OperationRecorederUserTable.GetAll();

            return result.ToOperationRecordersDto();
        }
    }
}
