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
        public Task<OperationResult<bool>> OpenShift(WorkingShiftModel shift);
        public Task<OperationResult<bool>> DepositAndWithdrawalMoney(WorkingShiftModel shift, Operation operation);
        public Task<OperationResult<bool>> CloseShift(WorkingShiftModel shift);
        public Task<OperationResult<WorkingShiftResourse>> GetWorkingShiftResourse(string fiscalNumberRRo);
        public Task<OperationResult<bool>> SendCheck(IEnumerable<ProductModel> products, Operation operation);
    }
}
