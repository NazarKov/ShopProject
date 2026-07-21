using ShopProjectWebServer.Api.DtoModels.Operation; 
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Common.Enum;
using ShopProjectWebServer.Services.Modules.Authorization;
using ShopProjectWebServer.Services.Modules.Domain.Operation.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.Operation;
using System.Threading.Tasks;
using OperationModel = ShopProjectWebServer.Models.Domain.Operation.Operation;

namespace ShopProjectWebServer.Services.Modules.Domain.Operation
{
    internal class OperationService : IOperationService
    {
        private DataBaseService _controller; 

        public OperationService(DataBaseService controller)
        {
            _controller = controller; 
        }
        public async Task<OperationResult<OperationModel>> Add(OperationModel item)
        {
            try
            {
                var result = await _controller.DataBaseAccess.OperationTable.AddAsync(item.ToOperationEntity());
                return OperationResult<OperationModel>.Success(result.ToOperation());
            }
            catch (Exception ex) 
            {
                return OperationResult<OperationModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        //public IEnumerable<OperationDto> GetAll(string token)
        //{
        //    if (!_authorizationServise.LoginToken(token))
        //    {
        //        throw new Exception("Невірний токен авторизації");
        //    } 
        //    var result = _controller.DataBaseAccess.OperationTable.GetAll();

        //    return result.ToOperationDto();
        //}

        public OperaiontStatisticsDto GetInfo(int shiftId)
        { 
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

        //public OperationІnformationDto GetInformation(string token, int shiftId)
        //{  
        //    var operation = new OperationEntity();
        //    if (shiftId == 0)
        //    {
        //        operation = _controller.DataBaseAccess.OperationTable.GetLatsItem();
        //    }
        //    else
        //    {
        //        try
        //        {
        //            operation = _controller.DataBaseAccess.OperationTable.GetLastItem(shiftId);
        //        }
        //        catch (InvalidOperationException invalidOperationException)
        //        {
        //            if (invalidOperationException.Message == "Sequence contains no elements")
        //            {
        //                operation = _controller.DataBaseAccess.OperationTable.GetLatsItem();
        //            }
        //            else
        //            {
        //                throw new Exception(invalidOperationException.Message);
        //            }
        //        }
        //    }
        //    var orders = _controller.DataBaseAccess.OrderTable.GetForOperation(operation.ID);

        //    var products = new List<ProductEntity>();
        //    foreach (var order in orders)
        //    {
        //        if (order.Product != null)
        //        {
        //            order.Product.Count = order.Count;
        //            products.Add(order.Product);
        //        }
        //    }

        //    operation.MAC = _controller.DataBaseAccess.MediaAccessControlTable.GetByOperationId(operation.ID);


        //    var result = new OperationІnformationDto()
        //    {
        //        Operation = operation.ToOperationDto(),
        //        // Products = products.ToProductDto()
        //    };
        //    if (operation.Discount != null)
        //    {
        //        result.Discount = operation.Discount.ToDiscount();
        //    }

        //    return result;
        //}

        //public OperationDto GetLast(string token, int shiftId)
        //{
        //    if (!_authorizationServise.LoginToken(token))
        //    {
        //        throw new Exception("Невірний токен авторизації");
        //    }
        //    var result = _controller.DataBaseAccess.OperationTable.GetLastItem(shiftId);

        //    return result.ToOperationDto();
        //}
    }
}
