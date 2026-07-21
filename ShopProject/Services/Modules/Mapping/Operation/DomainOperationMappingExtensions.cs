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
    }
}
