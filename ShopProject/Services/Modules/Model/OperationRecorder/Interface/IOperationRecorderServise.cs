using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.ObjectOwner;
using ShopProject.Model.UI.OperationRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.ModelService.OperationRecorder.Interface
{
    internal interface IOperationRecorderServise
    {
        public Task<List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>> GetAllOperationsRecorderOperationsUser();
        public  Task<Paginator<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>> GetOperationsRecorderPageColumn(int page, int countColumn, TypeStatusOperationRecorder status);
        public  Task<Paginator<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>> SearchByName(string item, int page, int countColumn, TypeStatusOperationRecorder status);
        public  Task<bool> GetServerSoftwareDeviceSettlementOperations(string pathFile, string passwordKey);
        public  Task<bool> SaveDataBaseItem(List<OperationRecorderDialogWindowModel> items);
        public List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> GetListObjecyOwner();
        public void ClearListObjectOwner();
        public  Task<bool> DeleteItem(ShopProject.Model.Domain.OperationRecorder.OperationRecorder item);
        public  Task<List<ObjectOwnerDialogWindowModel>> GetAllObjectOwner();
        public  Task<bool> SaveBinding(ShopProject.Model.Domain.OperationRecorder.OperationRecorder softwareDeviceSettlement, List<ObjectOwnerDialogWindowModel> objectOwnerHelpers);
        public void SetOperationRecorderOnWorkingShiftStatusInSession(ShopProject.Model.Domain.OperationRecorder.OperationRecorder operationRecorder);
        public ShopProject.Model.Domain.OperationRecorder.OperationRecorder GerOperationRecorderOnWorkingShiftStatusFromSession();
        public OperationRecorderSetting GetSetting();
    }
}
