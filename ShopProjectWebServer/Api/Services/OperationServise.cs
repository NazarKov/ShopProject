using ShopProjectWebServer.Api.DtoModels.Operation;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class OperationServise : IOperationServise
    {
        public int Add(string token, CreateOperationDto item)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            } 
            return DataBaseMainController.DataBaseAccess.OperationTable.Add(item.ToOperationEntiti());
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

        public OperaiontInfoDto GetInfo(string token, int shiftId)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var result = new OperaiontInfoDto()
            {
                AmountOfFundsIssued = DataBaseMainController.DataBaseAccess.OperationTable.GetTotalAmountOfFundsIssuedForShift(shiftId),
                AmountOfFundsReceived = DataBaseMainController.DataBaseAccess.OperationTable.GetTotalSumForShift(shiftId),
                TotalCheck = DataBaseMainController.DataBaseAccess.OperationTable.GetTotalOperationForShift(shiftId),
                AmountOfOfficialFundsIssued = DataBaseMainController.DataBaseAccess.OperationTable.GetAmountOfOfficialFundsIssuedCashForShift(shiftId),
                AmountOfOfficialFundsReceived = DataBaseMainController.DataBaseAccess.OperationTable.GetAmountOfOfficialFundsReceivedCashForShift(shiftId),
            };
            return result;
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
