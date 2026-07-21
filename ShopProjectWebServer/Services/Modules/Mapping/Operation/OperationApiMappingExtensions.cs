using ShopProjectWebServer.Api.DtoModels.Operation;
using ShopProjectWebServer.Models.Domain.Enum;
using ShopProjectWebServer.Services.Modules.Mapping.MediaAccessControl;
using OperationModel = ShopProjectWebServer.Models.Domain.Operation.Operation;

namespace ShopProjectWebServer.Services.Modules.Mapping.Operation
{
    public static class OperationApiMappingExtensions
    {
        public static OperationModel ToOperation(this CreateOperationDto operation)
        {
            var result = new OperationModel()
            {
                FiscalServerId = operation.FiscalServerId,
                BuyersAmount = operation.BuyersAmount,
                CreatedAt = operation.CreatedAt,
                Shift = new Models.Domain.WorkingShift.WorkingShift() { ID = operation.ShiftID },
                Discount = new Models.Domain.Discount.Discount() { ID = operation.DiscountID },
                GoodsTax = operation.GoodsTax,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment,
                TypeOperation = (TypeOperation)operation.TypeOperation,
                TypePayment = (TypePayment)operation.TypePayment,
            };
            if (operation.MAC != null)
            {
                result.MAC = operation.MAC.ToMediaAccessControl();
            }
            return result;
        }

        public static OperationDto ToOperationDto(this OperationModel operation)
        {
            var result = new OperationDto()
            {
                ID = operation.ID,
                FiscalServerId = operation.FiscalServerId,
                BuyersAmount = operation.BuyersAmount,
                CreatedAt = operation.CreatedAt,
                GoodsTax = operation.GoodsTax,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment,
                TypeOperation = (int)operation.TypeOperation,
                TypePayment = (int)operation.TypePayment,
            }; 
            return result;
        }
    }
}
