using ShopProjectDataBase.Entities;
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

        public OperaiontStatisticsDto GetInfo(string token, int shiftId)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var result = new OperaiontStatisticsDto()
            {
                AmountOfFundsIssued = DataBaseMainController.DataBaseAccess.OperationTable.GetTotalAmountOfFundsIssuedForShift(shiftId),
                AmountOfFundsReceived = DataBaseMainController.DataBaseAccess.OperationTable.GetTotalSumForShift(shiftId),
                TotalCheck = DataBaseMainController.DataBaseAccess.OperationTable.GetTotalOperationForShift(shiftId),
                AmountOfOfficialFundsIssued = DataBaseMainController.DataBaseAccess.OperationTable.GetAmountOfOfficialFundsIssuedCashForShift(shiftId),
                AmountOfOfficialFundsReceived = DataBaseMainController.DataBaseAccess.OperationTable.GetAmountOfOfficialFundsReceivedCashForShift(shiftId),
            };
            return result;
        }

        public OperationІnformationDto GetInformation(string token, int shiftId)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var operation = new OperationEntity();
            if(shiftId == 0)
            {
                operation = DataBaseMainController.DataBaseAccess.OperationTable.GetLatsItem();
            }
            else
            {
                try
                {
                    operation = DataBaseMainController.DataBaseAccess.OperationTable.GetLastItem(shiftId);
                }
                catch (InvalidOperationException invalidOperationException) 
                {
                    if(invalidOperationException.Message == "Sequence contains no elements")
                    {
                        operation = DataBaseMainController.DataBaseAccess.OperationTable.GetLatsItem();
                    }
                    else
                    {
                        throw new Exception(invalidOperationException.Message);
                    }
                }
            } 
            var orders = DataBaseMainController.DataBaseAccess.OrderTable.GetForOperation(operation.ID);

            var products = new List<ProductEntity>();
            foreach(var order in orders)
            {
                if (order.Product != null) 
                { 
                    order.Product.Count = order.Count;
                    products.Add(order.Product);
                }
            }

            operation.MAC = DataBaseMainController.DataBaseAccess.MediaAccessControlTable.GetByOperationId(operation.ID);

            var result = new OperationІnformationDto()
            {
                Operation = operation.ToOperationDto(),
                Products = products.ToProductDto()
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
