﻿using ShopProjectWebServer.Api.DtoModels.Operation;  
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
                MAC = new MediaAccessControlEntity() { ID = operation.MACID },
                Shift = new WorkingShiftEntity() { ID = operation.ShiftID },
                Discount = 0,
                GoodsTax = operation.GoodsTax,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment, 
            };

            Enum.TryParse(operation.TypeOperation.ToString(), out TypeOperation typeOperation);
            result.TypeOperation = typeOperation;
            Enum.TryParse(operation.TypePayment.ToString(), out TypeOperation TypePayment);
            result.TypeOperation = TypePayment; 
            return result;
        }
    }
}
