using ShopProjectWebServer.Api.DtoModels.Operation;  
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class OperaionMappingExtensions
    {
        public static OperationEntity ToOperationEntiti(this CreateOperationDto operation)
        {  
            var result = new OperationEntity()
            {
                AmountOfFundsReceived = operation.AmountOfFundsReceived,
                BuyersAmount = operation.BuyersAmount,
                AmountOfIssuedFunds = operation.AmountOfIssuedFunds,
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
            Enum.TryParse(operation.TypePayment.ToString(), out TypeOperation TypePayment);
            result.TypeOperation = TypePayment; 
            return result;
        }

        public static OperationDto ToOperationDto(this OperationEntity operation)
        {
            var item = new OperationDto()
            {
                AmountOfFundsReceived = operation.AmountOfFundsReceived,
                BuyersAmount = operation.BuyersAmount,
                CreatedAt = operation.CreatedAt, 
                GoodsTax = operation.GoodsTax,
                MACId = operation.MACId,
                ID = operation.ID,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment,
                TypeOperation = (int)operation.TypeOperation,
                TypePayment = (int)operation.TypePayment,
            };
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
