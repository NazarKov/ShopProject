using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Enum; 
using ShopProject.Services.Modules.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using OperationRecorderModel = ShopProject.Model.Domain.OperationRecorder.OperationRecorder;

namespace ShopProject.Services.Modules.Domain.OperationRecorder.Interface
{
    internal interface IOperationRecorderService
    {
        public Task<OperationResult<OperationRecorderModel>> Add(OperationRecorderModel Item);
        public Task<OperationResult<IEnumerable<OperationRecorderModel>>> AddRange(IEnumerable<OperationRecorderModel> Items);

        public Task<OperationResult<Paginator<OperationRecorderModel, TypeStatusOperationRecorder>>> GetPageColumn(int page, int countColumn, TypeStatusOperationRecorder status);
        public Task<OperationResult<Paginator<OperationRecorderModel, TypeStatusOperationRecorder>>> SearchByName(string item, int page, int countColumn, TypeStatusOperationRecorder status);

        public Task<OperationResult<IEnumerable<OperationRecorderModel>>> GetTaxServer(string pathFile, string passwordKey);
        /// <summary>
        /// 
        /// </summary> 


        public Task<List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>> GetAllOperationsRecorderOperationsUser();  
        public List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> GetListObjecyOwner();
        public void ClearListObjectOwner();
        public  Task<bool> DeleteItem(ShopProject.Model.Domain.OperationRecorder.OperationRecorder item);
        //public  Task<List<TaxObjectSelectItemModel>> GetAllObjectOwner();
        //public  Task<bool> SaveBinding(ShopProject.Model.Domain.OperationRecorder.OperationRecorder softwareDeviceSettlement, List<TaxObjectSelectItemModel> objectOwnerHelpers);
        public void SetOperationRecorderOnWorkingShiftStatusInSession(ShopProject.Model.Domain.OperationRecorder.OperationRecorder operationRecorder);
        public ShopProject.Model.Domain.OperationRecorder.OperationRecorder GerOperationRecorderOnWorkingShiftStatusFromSession();
        public OperationRecorderSetting GetSetting();
    }
}
