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
                MAC = new MediaAccessControlEntity() { ID = operation.MACID },
                Shift = new WorkingShiftEntity() { ID = operation.ShiftID },
                Discount = 0,
                GoodsTax = operation.GoodsTax,
                NumberPayment = operation.NumberPayment,
                RestPayment = operation.RestPayment,
                TotalPayment = operation.TotalPayment, 
            };

            switch (operation.TypePayment)
            {
                case TypePaymentDto.None:
                    {
                        result.TypePayment = TypePayment.None;
                        break;
                    }
                case TypePaymentDto.Cash: 
                    {
                        result.TypePayment = TypePayment.Cash;
                        break;
                    }
                case TypePaymentDto.Card:
                    {
                        result.TypePayment = TypePayment.Card;
                        break;
                    }
                case TypePaymentDto.GiftCertificate:
                    {
                        result.TypePayment = TypePayment.GiftCertificate;
                        break;
                } 
            }

            switch (operation.TypeOperation)
            {
                case TypeOperationDto.None:
                    {
                        result.TypeOperation = TypeOperation.None;
                        break;
                    }
                case TypeOperationDto.FiscalCheck:
                    {
                        result.TypeOperation = TypeOperation.FiscalCheck;
                        break;
                    }
                case TypeOperationDto.ReturnCheck:
                    {
                        result.TypeOperation = TypeOperation.ReturnCheck;
                        break;
                    }
                case TypeOperationDto.DepositMoney:
                    {
                        result.TypeOperation = TypeOperation.DepositMoney;
                        break;
                    }
                case TypeOperationDto.WithdrawalMoney:
                    {
                        result.TypeOperation = TypeOperation.WithdrawalMoney;
                        break;
                    }
            }
             
            return result;
        }
    }
}
