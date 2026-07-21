using ShopProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.Operation
{
    public class OperationSaleInfo
    {
        public decimal TotalSum;
        public decimal? SumaOrder;
        public decimal? SumaUser;
        public decimal Discount;
        public decimal DiscountPrecent;
        public TypePayment TypePayment;
        public IEnumerable<ShopProject.Model.Domain.Product.Product>? Products;

        public bool DrawingCheck;
        public bool IsFiscalCheck;
    }
}
