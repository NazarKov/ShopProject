using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Operation;
using ShopProject.UIModel;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class OperaionMappingExtensions
    {
        public static CreateOperationDto ToCreateOperationDto(this UIOperationModel operation)
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

            switch (operation.TypePayment)
            {
                case TypePayment.None:
                    {
                        result.TypePayment = TypePaymentDto.None;
                        break;
                    }
                case TypePayment.Cash:
                    {
                        result.TypePayment = TypePaymentDto.Cash;
                        break;
                    }
                case TypePayment.Card:
                    {
                        result.TypePayment = TypePaymentDto.Card;
                        break;
                    }
                case TypePayment.GiftCertificate:
                    {
                        result.TypePayment = TypePaymentDto.GiftCertificate;
                        break;
                    }
            }

            switch (operation.TypeOperation)
            {
                case TypeOperation.None:
                    {
                        result.TypeOperation = TypeOperationDto.None;
                        break;
                    }
                case TypeOperation.FiscalCheck:
                    {
                        result.TypeOperation = TypeOperationDto.FiscalCheck;
                        break;
                    }
                case TypeOperation.ReturnCheck:
                    {
                        result.TypeOperation = TypeOperationDto.ReturnCheck;
                        break;
                    }
                case TypeOperation.DepositMoney:
                    {
                        result.TypeOperation = TypeOperationDto.DepositMoney;
                        break;
                    }
                case TypeOperation.WithdrawalMoney:
                    {
                        result.TypeOperation = TypeOperationDto.WithdrawalMoney;
                        break;
                    }
            }

            return result; 
        }
    }
}
