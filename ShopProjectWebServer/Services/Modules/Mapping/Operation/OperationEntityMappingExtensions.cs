using ShopProjectDataBase.Entities; 
using ShopProjectWebServer.Models.Domain.Enum;
using ShopProjectWebServer.Services.Modules.Mapping.MediaAccessControl;
using OperationModel = ShopProjectWebServer.Models.Domain.Operation.Operation;

namespace ShopProjectWebServer.Services.Modules.Mapping.Operation
{
    public static class OperationEntityMappingExtensions
    {


        public static OperationEntity ToOperationEntity(this OperationModel operation)
        {
            var item = new OperationEntity()
            {
                FiscalServerId = operation.FiscalServerId,
                BuyersAmount = operation.BuyersAmount,
                CreatedAt = operation.CreatedAt,
                GoodsTax = operation.GoodsTax,
                ID = operation.ID,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment,
                TypeOperation = (ShopProjectDataBase.Helper.TypeOperation)operation.TypeOperation,
                TypePayment = (ShopProjectDataBase.Helper.TypePayment)operation.TypePayment, 
            };
            if (operation.MAC != null)
            {
                item.MAC = operation.MAC.ToMediaAccessControlEntity();
            }
            if (operation.Discount != null)
            {
                item.Discount = new DiscountEntity() { ID = operation.Discount.ID };
            }
            if(operation.Shift != null)
            {
                item.Shift = new WorkingShiftEntity() { ID = operation.Shift.ID };
            }
            return item;
        }
        public static IEnumerable<OperationEntity> ToOperationEntity(this IEnumerable<OperationModel> operations)
        {
            var result = new List<OperationEntity>();
            foreach (var item in operations)
            {
                result.Add(ToOperationEntity(item));
            }
            return result;
        }

        public static OperationModel ToOperation(this OperationEntity operation)
        {
            var item = new OperationModel()
            {
                FiscalServerId = operation.FiscalServerId,
                BuyersAmount = operation.BuyersAmount,
                CreatedAt = operation.CreatedAt,
                GoodsTax = operation.GoodsTax,
                ID = operation.ID,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment,
                TypeOperation = (TypeOperation)operation.TypeOperation,
                TypePayment = (TypePayment)operation.TypePayment,
            };
            if (operation.MAC != null)
            {
                item.MAC = operation.MAC.ToMediaAccessControl();
            }
            if (operation.Discount != null)
            {
                item.Discount = new Models.Domain.Discount.Discount() { ID = operation.Discount.ID };
            }
            return item;
        }

    }
}
