using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.Operation;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class OperationServise : IOperationServise
    {
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public OperationServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
        }
        public int Add(string token, CreateOperationDto item)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            } 
            return _controller.DataBaseAccess.OperationTable.Add(item.ToOperationEntiti());
        }

        public IEnumerable<OperationDto> GetAll(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            } 
            var result = _controller.DataBaseAccess.OperationTable.GetAll();

            return result.ToOperationDto();
        }

        public OperaiontStatisticsDto GetInfo(string token, int shiftId)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var result = new OperaiontStatisticsDto()
            {
                AmountOfFundsIssued = _controller.DataBaseAccess.OperationTable.GetTotalAmountOfFundsIssuedForShift(shiftId),
                AmountOfFundsReceived = _controller.DataBaseAccess.OperationTable.GetTotalSumForShift(shiftId),
                TotalCheck = _controller.DataBaseAccess.OperationTable.GetTotalOperationForShift(shiftId),
                AmountOfOfficialFundsIssued = _controller.DataBaseAccess.OperationTable.GetAmountOfOfficialFundsIssuedCashForShift(shiftId),
                AmountOfOfficialFundsReceived = _controller.DataBaseAccess.OperationTable.GetAmountOfOfficialFundsReceivedCashForShift(shiftId),
                TotalReturnCheck = _controller.DataBaseAccess.OperationTable.GetTotalReturnOperationForShift(shiftId),
            };
            return result;
        }

        public OperationІnformationDto GetInformation(string token, int shiftId)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var operation = new OperationEntity();
            if(shiftId == 0)
            {
                operation = _controller.DataBaseAccess.OperationTable.GetLatsItem();
            }
            else
            {
                try
                {
                    operation = _controller.DataBaseAccess.OperationTable.GetLastItem(shiftId);
                }
                catch (InvalidOperationException invalidOperationException) 
                {
                    if(invalidOperationException.Message == "Sequence contains no elements")
                    {
                        operation = _controller.DataBaseAccess.OperationTable.GetLatsItem();
                    }
                    else
                    {
                        throw new Exception(invalidOperationException.Message);
                    }
                }
            } 
            var orders = _controller.DataBaseAccess.OrderTable.GetForOperation(operation.ID);

            var products = new List<ProductEntity>();
            foreach(var order in orders)
            {
                if (order.Product != null) 
                { 
                    order.Product.Count = order.Count;
                    products.Add(order.Product);
                }
            }

            operation.MAC = _controller.DataBaseAccess.MediaAccessControlTable.GetByOperationId(operation.ID);

            var result = new OperationІnformationDto()
            {
                Operation = operation.ToOperationDto(),
                Products = products.ToProductDto()
            };
            return result;
        }

        public OperationDto GetLast(string token, int shiftId)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var result = _controller.DataBaseAccess.OperationTable.GetLastItem(shiftId);

            return result.ToOperationDto();
        }
    }
}
