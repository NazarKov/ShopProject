using DocumentFormat.OpenXml.Spreadsheet;
using ShopProject.Helpers;
using ShopProject.Helpers.FileServise.XmlServise;
using ShopProject.Helpers.NetworkServise.FiscalServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.PrintingServise;
using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.UserPage;
using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.SalePage
{
    internal class WorkShiftMenuModel
    {  
        private MainFiscalServerController _fiscalOperationController;
        private readonly string _token; 
        public WorkShiftMenuModel()
        {   
            _fiscalOperationController = new MainFiscalServerController();
            _token = Session.User.Token; 
        }

        public async Task<bool> OpenShift(WorkingShift shiftEntity)
        {
            try
            { 
                if (_fiscalOperationController.OpenShift(shiftEntity) == "OK")
                {
                    Session.WorkingShiftStatus.WorkingShift = shiftEntity;
                    shiftEntity.MACCreateAt = CreateMac(shiftEntity);
                    Session.WorkingShiftStatus.WorkingShift.ID = await SaveDataBaseOpenShift(shiftEntity);  
                    return true;
                }
                return false;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void AddKey(SignatureKey key) => _fiscalOperationController.AddKey(key);

        public async Task<bool> CloseShift(WorkingShift shiftEntity)
        { 
            if (_fiscalOperationController.CloseShift(shiftEntity) == "OK")
            {
                shiftEntity.ID = Session.WorkingShiftStatus.WorkingShift.ID;
                shiftEntity.MACEndAt = CreateMac(shiftEntity);
                await SaveDataBaseCloseShift(shiftEntity); 
                return true;
            }
            return false;
        }
        public async Task<bool> DepositAndWithdrawalMoney(WorkingShift shift,Operation operation)
        {
            if (_fiscalOperationController.DepositAndWithdrawalMoney(shift, operation) == "OK")
            {
                operation.Shift = Session.WorkingShiftStatus.WorkingShift;
                operation.MAC = CreateMac(shift);
                await SaveDataBaseOperation(operation); 
                return true;
            }
            return false;
        }


        private async Task<int> SaveDataBaseOperation(Operation operation){
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.OperationController.AddOperation(_token, operation);
            }
            catch
            {
                return 0;
            }
        }
        private async Task<int> SaveDataBaseOpenShift(WorkingShift shift)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.WorkingShiftContoller.AddWorkingShift(_token, shift);
            }
            catch
            {
                return 0;
            }
        }
        private async Task<bool> SaveDataBaseCloseShift(WorkingShift shift)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.WorkingShiftContoller.UpdateWorkingShift(_token, shift);
            }
            catch
            {
                return false;
            }
        }

        private MediaAccessControl CreateMac(WorkingShift workingShift ,Operation operation = null)
        {
            return   new MediaAccessControl()
            {
                OperationsRecorder = Session.WorkingShiftStatus.OperationRecorder,
                Content = XmlServise.GenerationMACForXML(),
                WorkingShifts = workingShift,
                Operation = operation
            }; 
        }  

        public async Task<MediaAccessControl> GetMAC(Guid operationRecorderId)
        {
            try
            {
                var mac = Session.WorkingShiftStatus.MediaAccessControl;
                if(mac != null && mac.Content!= string.Empty)
                {
                    return mac;
                }
                else
                {
                    return (await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.GetLastMAC(_token, operationRecorderId)).ToUIMediaAccessControl();
                }

            } 
            catch
            {
                MessageBox.Show("Невдалося отримати MAC");
                return new MediaAccessControl();
            }
        }

        public async Task<WorkingShift> GetWorkingShift(string id)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.WorkingShiftContoller.GetWorkingShift(_token, id);
                return result.ToWorkingShift();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public async Task<OperationInfo> GetOperationInfo(int id)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.OperationController.GetOperationsInfo(_token, id);
                return result.ToOperationInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public async Task<string> GetLocalNumber()
        {
            try
            {
                var item = await MainWebServerController.MainDataBaseConntroller.OperationController.GetLastNumberOperation(_token, Session.WorkingShiftStatus.WorkingShift.ID);

                if (item == string.Empty)
                {
                    return "1";
                }
                else
                {
                    return (Convert.ToInt32(item) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                return "1";
            }
        }
    }
}
