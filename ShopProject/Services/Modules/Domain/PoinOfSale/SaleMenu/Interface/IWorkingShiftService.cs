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
        public Task<OperationResult<bool>> OpenShift();
        public Task<OperationResult<bool>> DepositAndWithdrawalMoney(decimal cash, TypeOperation typeOperation);
        public Task<OperationResult<bool>> CloseShift();
        public void SetWorkingShiftStatusOnSession(ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus item);
        public ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus GetWorkingShiftStatusFromSession();
        public void SetWorkingShiftStatusOnSetting(ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus item);
        public ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus GetWorkingShiftStatusFromSetting();

        public bool IsTestMode(); 
        //public void AddKey(SignatureKey key);  
        //public  Task<ShopProject.Model.Domain.WorkingShift.WorkingShift> GetWorkingShift(string id);
        //public  Task<OperationInfo> GetOperationInfo(int id); 
        //public  Task PrintLastCheck();
        //public void LoadSaleMenuDataFromFile();

        //public void SetTabsOnSession(ObservableCollection<TabItem> items);
        //public ObservableCollection<TabItem> GetTabsFromSession();
    }
}
