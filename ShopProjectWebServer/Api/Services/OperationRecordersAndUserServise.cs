using ShopProjectDataBase.Entities; 
using ShopProjectWebServer.Api.DtoModels.OperationRecorder; 
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
         
        public OperationRecorderUserDto GetOperationRecorderForUser(string token) 
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
             
            var items = DataBaseMainController.DataBaseAccess.OperationRecorederUserTable.GetAll();

            var user = DataBaseMainController.DataBaseAccess.UserTable.GetUser(token);
            var operationRecoreder = new List<OperationsRecorderEntity>();

            foreach (var item in items)
            { 
                if(item.Users.ID == user.ID)
                {
                    operationRecoreder.Add(item.OpertionsRecorders);
                }
            }

            var result = new OperationRecorderUserDto()
            {
                User = user.ToUserDto(),
                OpertionsRecorders = operationRecoreder.ToOperationRecorderDto(),
            }; 
            return result; 
        }
    }
}
