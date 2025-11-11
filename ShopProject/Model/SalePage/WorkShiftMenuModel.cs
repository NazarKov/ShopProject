using ShopProject.Helpers;
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
        private PrintingDayReport _printingController;
        private MainFiscalServerController _fiscalOperationController;
        private readonly string _token;
        public WorkShiftMenuModel()
        {  

            _printingController = new PrintingDayReport();

            _fiscalOperationController = new MainFiscalServerController();
            _token = Session.User.Token;
        }

        public bool OpenShift(WorkingShift shiftEntity)
        {
            try
            {
                if (_fiscalOperationController.OpenShift(shiftEntity) == "OK")
                {
                    Session.WorkingShift = shiftEntity;
                    Task.Run(async () =>
                    {
                        Session.WorkingShift.ID = await SaveDataBaseOpenShift(shiftEntity);
                        await CreateMac(shiftEntity);
                    });
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

        public bool CloseShift(WorkingShift shiftEntity)
        {
            if (_fiscalOperationController.CloseShift(shiftEntity) == "OK")
            {
                Task.Run(async () =>
                {
                    await SaveDataBaseCloseShift(shiftEntity);
                    await CreateMac(shiftEntity);
                });
                return true;
            }
            return false;
        }
        public bool OfficialDepositMoney(OperationEntity operation)
        {
            //if (_fiscalOperationController.DepositAndWithdrawalMoney(operation) == "OK")
            //{
            //    SaveDataBase(operation);
            //    return true;
            //}
            return false;
        }
        private async Task<int> SaveDataBaseOpenShift(WorkingShift shift)
        {
            return await MainWebServerController.MainDataBaseConntroller.WorkingShiftContoller.AddWorkingShift(_token, shift);
        }
        private async Task<bool> SaveDataBaseCloseShift(WorkingShift shift)
        {
            return await MainWebServerController.MainDataBaseConntroller.WorkingShiftContoller.UpdateWorkingShift(_token, shift);
        }

        private async Task<bool?> CreateMac(WorkingShift workingShift )
        {

            var mac = WriteReadXmlFile.GenerationMACForXML();

            if (mac != null)
            {
                return await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.AddMAC(_token, new MediaAccessControl()
                {
                    OperationsRecorder = Session.FocusDevices,
                    Content = mac,
                    WorkingShifts = workingShift,
                });
            }

            return false;
        }  

        public async Task<MediaAccessControl> GetMAC(Guid operationRecorderId)
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.GetLastMAC(_token, operationRecorderId)).ToUIMediaAccessControl();
            } 
            catch (Exception ex)
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
    }
}
