using ShopProject.Model.Domain.Operation;
using ShopProject.Model.UI.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.Operation
{
    public static class DomainOperationMappingExtensions
    {
        public static OperationSaleInfo ToOperationInfoSale(this OperationSaleInfoModel item)
        {
            var result = new OperationSaleInfo()
            {
                SumaOrder = item.SumaOrder,
                SumaUser = item.SumaUser,
                TotalSum = item.TotalSum,
                Discount = item.Discount,
                DiscountPrecent = item.DiscountPrecent,
                DrawingCheck = item.DrawingCheck,
                IsFiscalCheck = item.IsFiscalCheck,
                TypePayment = item.TypePayment, 
            };

            var products = new List<ShopProject.Model.Domain.Product.Product>();
            foreach(var product in item.Products)
            {
                products.Add(product.Product);
            }
            result.Products = products;
            return result;
        }

        public static OperationModel ToOperationModel(this ShopProject.Model.Domain.Operation.Operation item)
        {
            return new OperationModel()
            {
                ID = item.ID,
                BuyersAmount = item.BuyersAmount,
                CreatedAt = item.CreatedAt,
                FiscalServerId = item.FiscalServerId,
                GoodsTax = item.GoodsTax,
                NumberPayment = item.NumberPayment,
                RestPayment = item.RestPayment,
                TotalPayment = item.TotalPayment,
                TypeOperation = item.TypeOperation,
                TypePayment = item.TypePayment
            };
        }
        public static OperationsInfoModel ToOperationInfoModel(this OperationInfo item)
        {
            return new OperationsInfoModel()
            {
                AmountOfFundsIssued = item.AmountOfFundsIssued,
                AmountOfFundsReceived = item.AmountOfFundsReceived,
                AmountOfOfficialFundsIssued = item.AmountOfOfficialFundsIssued,
                AmountOfOfficialFundsReceived = item.AmountOfOfficialFundsReceived,
                TotalCheck = item.TotalCheck,
                TotalReturnCheck = item.TotalReturnCheck,
            };
        }
    }
}
