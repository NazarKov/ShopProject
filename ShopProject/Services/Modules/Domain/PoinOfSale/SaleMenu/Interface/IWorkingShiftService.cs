using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.Enum;
using ShopProject.Services.Modules.Common;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface
{
    internal interface IWorkingShiftService
    {
        public Task<OperationResult<string>> OpenShift();
        public Task<OperationResult<string>> DepositAndWithdrawalMoney(decimal cash, TypeOperation typeOperation);
        public Task<OperationResult<string>> CloseShift(); 
        public ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus GetWorkingShiftStatusFromSession(); 
        public ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus GetWorkingShiftStatusFromSetting(); 
        public Task<OperationInfo> GetOperationInfo(int id);
        public Operation GetOperationSession();
        public bool IsTestMode();
        public Task<OperationResult<bool>> PrintLastCheck();
    }
}
