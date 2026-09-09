using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.WorkingShift; 
using ShopProject.Services.Modules.Common;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using WorkingShiftModel = ShopProject.Model.Domain.WorkingShift.WorkingShift;
using ProductModel = ShopProject.Model.Domain.Product.Product;
namespace ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface
{
    internal interface IWorkingShfitOperationService
    {
        public Task<OperationResult<string>> OpenShift(WorkingShiftModel shift);
        public Task<OperationResult<string>> DepositAndWithdrawalMoney(WorkingShiftModel shift, Operation operation);
        public Task<OperationResult<string>> CloseShift(WorkingShiftModel shift);
        public Task<OperationResult<WorkingShiftResourse>> GetWorkingShiftResourse(string fiscalNumberRRo);
        public Task<OperationResult<string>> SendCheck(IEnumerable<ProductModel> products, Operation operation);
    }
}
