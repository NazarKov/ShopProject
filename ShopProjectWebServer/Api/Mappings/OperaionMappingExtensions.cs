using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.DtoModels.Operation;  
using System.Security.Cryptography;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class OperaionMappingExtensions
    {
        public static OperationEntity ToOperationEntiti(this CreateOperationDto operation)
        {  
            var result = new OperationEntity()
            { 
                FiscalServerId = operation.FiscalServerId,
                BuyersAmount = operation.BuyersAmount, 
                CreatedAt = operation.CreatedAt, 
                Shift = new WorkingShiftEntity() { ID = operation.ShiftID },
                Discount = new DiscountEntity() { ID = operation.DiscountID},
                GoodsTax = operation.GoodsTax,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment, 
            };
            if(operation.MAC!=null) 
            {
                result.MAC = operation.MAC.ToMediaAccessEntity();
            }

            Enum.TryParse(operation.TypeOperation.ToString(), out TypeOperation typeOperation);
            result.TypeOperation = typeOperation;
            Enum.TryParse(operation.TypePayment.ToString(), out TypePayment TypePayment);
            result.TypePayment = TypePayment; 
            return result;
        }

        public static OperationDto ToOperationDto(this OperationEntity operation)
        {
            var item = new OperationDto()
            { 
                FiscalServerId = operation.FiscalServerId,
                BuyersAmount = operation.BuyersAmount,
                CreatedAt = operation.CreatedAt, 
                GoodsTax = operation.GoodsTax, 
                ID = operation.ID,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment,
                TypeOperation = (int)operation.TypeOperation,
                TypePayment = (int)operation.TypePayment,
            };
            if (operation.MAC!=null)
            {
                item.MAC = operation.MAC.ToMediaAccessDto();
            }
            if (operation.Discount != null) 
            {
                item.DiscountID = operation.Discount.ID;
            }
            return item;
        }
        public static IEnumerable<OperationDto> ToOperationDto(this IEnumerable<OperationEntity> operations)
        {
            var result = new List<OperationDto>();
            foreach (var item in operations)
            {
                result.Add(ToOperationDto(item));
            }
            return result;
        }
    }
}
