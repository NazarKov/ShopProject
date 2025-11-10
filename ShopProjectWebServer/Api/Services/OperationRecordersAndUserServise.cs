using ShopProjectDataBase.Entities;
<<<<<<< HEAD
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
=======
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
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

<<<<<<< HEAD
        public OperationRecorderUserDto GetOperationRecorderForUser(string token)
=======
        public IEnumerable<OperationRecorderUserDto> GetAll(string token)
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

<<<<<<< HEAD
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
=======
            var result = DataBaseMainController.DataBaseAccess.OperationRecorederUserTable.GetAll();

            return result.ToOperationRecordersDto();
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        }
    }
}
