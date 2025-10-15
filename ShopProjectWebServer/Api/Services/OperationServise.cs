using ShopProjectWebServer.Api.DtoModels.Operation;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class OperationServise : IOperationServise
    {
        public void Add(string token, CreateOperationDto item)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            } 
            DataBaseMainController.DataBaseAccess.OperationTable.Add(item.ToOperationEntiti());
        }

        public IEnumerable<OperationDto> GetAll(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            } 
            var result = DataBaseMainController.DataBaseAccess.OperationTable.GetAll();

            return result.ToOperationDto();
        }

        public OperationDto GetLast(string token, int shiftId)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var result = DataBaseMainController.DataBaseAccess.OperationTable.GetLastItem(shiftId);

            return result.ToOperationDto();
        }
    }
}
