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
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public OperationRecordersAndUserServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
        }
        public void Add(string token, Guid userID, IEnumerable<BindingUserToOperationRecorderDto> operationRecorders)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            _controller.DataBaseAccess.OperationRecorederUserTable.AddRange(userID, operationRecorders.ToOperationRecordersEntity());
        }
         
        public OperationRecorderUserDto GetOperationRecorderForUser(string token) 
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
             
            var items = _controller.DataBaseAccess.OperationRecorederUserTable.GetAll();

            var user = _controller.DataBaseAccess.UserTable.GetUser(token);
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
