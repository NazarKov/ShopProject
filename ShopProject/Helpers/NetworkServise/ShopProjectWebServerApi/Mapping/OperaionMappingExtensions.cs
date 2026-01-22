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
                FiscalServerId = operation.FiscalServerId,
                BuyersAmount = operation.BuyersAmount,
                CreatedAt = operation.CreatedAt,
                GoodsTax = operation.GoodsTax,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment,
            };
            if (operation.MAC != null)
            {
                result.MAC = operation.MAC.ToCreatMediaAccessControlDto();
            }
            if (operation.Shift != null)
            {
                result.ShiftID = operation.Shift.ID;
            }
            if (operation.Discount != null)
            {
                result.DiscountID = operation.Discount.ID;
            }

            result.TypePayment = (int)operation.TypeOperation;
            result.TypeOperation = (int)operation.TypeOperation;
            return result;
        }
        public static Operation ToOperation(this OperationDto operation)
        {
            var result = new Operation()
            {
                FiscalServerId = operation.FiscalServerId,
                BuyersAmount = operation.BuyersAmount,
                CreatedAt = operation.CreatedAt,
                GoodsTax = operation.GoodsTax,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment,
            };
            if (operation.MAC != null)
            {
                result.MAC = operation.MAC.ToUIMediaAccessControl();
            }  
            result.TypePayment = (TypePayment)operation.TypeOperation;
            result.TypeOperation = (TypeOperation)operation.TypeOperation;
            return result;
        }
        public static OperationInfo ToOperationInfo(this OperationInfoDto info)
        {
            var result = new OperationInfo()
            { 
                AmountOfFundsIssued = info.AmountOfFundsIssued,
                AmountOfFundsReceived = info.AmountOfFundsReceived,
                TotalCheck = info.TotalCheck,
                AmountOfOfficialFundsReceived = info.AmountOfOfficialFundsReceived,
                AmountOfOfficialFundsIssued = info.AmountOfOfficialFundsIssued
            };
            return result;
        }
    }
}
