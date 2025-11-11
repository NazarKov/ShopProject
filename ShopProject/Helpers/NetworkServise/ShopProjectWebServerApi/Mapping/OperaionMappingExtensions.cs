using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Operation;
using ShopProject.UIModel.SalePage;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class OperaionMappingExtensions
    {
        public static CreateOperationDto ToCreateOperationDto(this Operation operation)
        {
            var result = new CreateOperationDto()
            {
                AmountOfFundsReceived = operation.AmountOfFundsReceived,
                BuyersAmount = operation.BuyersAmount,
                AmountOfIssuedFunds = operation.AmountOfIssuedFunds,
                CreatedAt = operation.CreatedAt,
                MACID = operation.MAC.ID,
                ShiftID = operation.Shift.ID,
                Discount = 0,
                GoodsTax = operation.GoodsTax,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment,
            };
            result.TypePayment = (int)operation.TypeOperation;
            result.TypeOperation = (int)operation.TypeOperation; 
            return result; 
        } 
    }
}
