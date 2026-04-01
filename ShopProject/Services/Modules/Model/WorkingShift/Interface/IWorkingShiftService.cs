using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.SignatureKey;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopProject.Services.Modules.Model.WorkingShift.Interface
{
    internal interface IWorkingShiftService
    {
        public Task<bool> OpenShift(ShopProject.Model.Domain.WorkingShift.WorkingShift shiftEntity);
        public Task<bool> CloseShift(ShopProject.Model.Domain.WorkingShift.WorkingShift shiftEntity);
        public void AddKey(SignatureKey key);
        public  Task<bool> DepositAndWithdrawalMoney(ShopProject.Model.Domain.WorkingShift.WorkingShift shift, Operation operation);
        public  Task<MediaAccessControl> GetMAC(Guid operationRecorderId);
        public  Task<ShopProject.Model.Domain.WorkingShift.WorkingShift> GetWorkingShift(string id);
        public  Task<OperationInfo> GetOperationInfo(int id);
        public  Task<string> GetLocalNumber();
        public  Task PrintLastCheck();
        public void LoadSaleMenuDataFromFile();
        public void SetWorkingShiftStatusOnSession(ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus item);
        public ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus GetWorkingShiftStatusFromSession();
        public void SetWorkingShiftStatusOnSetting(ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus item);
        public ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus GetWorkingShiftStatusFromSetting();
        public void SetTabsOnSession(ObservableCollection<TabItem> items);
        public ObservableCollection<TabItem> GetTabsFromSession();
    }
}
