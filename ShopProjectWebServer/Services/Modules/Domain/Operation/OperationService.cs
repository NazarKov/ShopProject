using ShopProjectWebServer.Api.DtoModels.Operation; 
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Models.Domain.Product;
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Common.Enum;
using ShopProjectWebServer.Services.Modules.Authorization;
using ShopProjectWebServer.Services.Modules.Domain.Operation.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.Discount;
using ShopProjectWebServer.Services.Modules.Mapping.MediaAccessControl;
using ShopProjectWebServer.Services.Modules.Mapping.Operation;
using ShopProjectWebServer.Services.Modules.Mapping.Product;
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

        public async Task<OperationResult<OperaiontStatisticsDto>> GetInfo(int shiftId)
        { 
            return OperationResult<OperaiontStatisticsDto>.Success(new OperaiontStatisticsDto()
            {
                AmountOfFundsIssued = _controller.DataBaseAccess.OperationTable.GetTotalAmountOfFundsIssuedForShift(shiftId),
                AmountOfFundsReceived = _controller.DataBaseAccess.OperationTable.GetTotalSumForShift(shiftId),
                TotalCheck = _controller.DataBaseAccess.OperationTable.GetTotalOperationForShift(shiftId),
                AmountOfOfficialFundsIssued = _controller.DataBaseAccess.OperationTable.GetAmountOfOfficialFundsIssuedCashForShift(shiftId),
                AmountOfOfficialFundsReceived = _controller.DataBaseAccess.OperationTable.GetAmountOfOfficialFundsReceivedCashForShift(shiftId),
                TotalReturnCheck = _controller.DataBaseAccess.OperationTable.GetTotalReturnOperationForShift(shiftId),
            }); 
        }

        public async Task<OperationResult<OperationІnformationDto>> GetInformation(int shiftId)
        {
            var operation = new OperationModel();
            if (shiftId == 0)
            {
                return OperationResult<OperationІnformationDto>.Fail("Невдалося завантажити чек");
            }
            else
            {
                try
                {
                    operation = _controller.DataBaseAccess.OperationTable.GetLastItem(shiftId).ToOperation();
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    if (invalidOperationException.Message == "Sequence contains no elements")
                    {
                        operation = _controller.DataBaseAccess.OperationTable.GetLatsItem().ToOperation();
                    }
                    else
                    {
                        throw new Exception(invalidOperationException.Message);
                    }
                }
            }
            var orders = _controller.DataBaseAccess.OrderTable.GetForOperation(operation.ID);

            var products = new List<ShopProjectWebServer.Models.Domain.Product.Product>();
            foreach (var order in orders)
            {
                if (order.Product != null)
                {
                    order.Product.Count = order.Count;
                    products.Add(order.Product.ToProduct());
                }
            }

            operation.MAC = _controller.DataBaseAccess.MediaAccessControlTable.GetByOperationId(operation.ID).ToMediaAccessControl();


            var result = new OperationІnformationDto()
            {
                Operation = operation.ToOperationDto(),
                Products = products.ToProductDto()
            };
            if (operation.Discount != null)
            {
                result.Discount = operation.Discount.ToDiscountDto();
            }

            return OperationResult<OperationІnformationDto>.Success(result);
        } 
    }
}
