using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.FiscalServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.PrintingServise;
using ShopProject.UIModel.SalePage;
using ShopProjectSQLDataBase.Entities;
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

        public WorkShiftMenuModel()
        {  
            _printingController = new PrintingDayReport();

            _fiscalOperationController = new MainFiscalServerController();
        }

        public bool OpenShift(UIWorkingShiftModel shiftEntity)
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

        public void AddKey(ElectronicSignatureKey key) => _fiscalOperationController.AddKey(key);

        public bool CloseShift(UIWorkingShiftModel shiftEntity)
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
        private async Task<int> SaveDataBaseOpenShift(UIWorkingShiftModel shift)
        {
            return await MainWebServerController.MainDataBaseConntroller.WorkingShiftContoller.AddWorkingShift(Session.Token, shift);
        }
        private async Task<bool> SaveDataBaseCloseShift(UIWorkingShiftModel shift)
        {
            return await MainWebServerController.MainDataBaseConntroller.WorkingShiftContoller.UpdateWorkingShift(Session.Token, shift);
        }

        private async Task<bool?> CreateMac(UIWorkingShiftModel workingShift )
        {

            var mac = WriteReadXmlFile.GenerationMACForXML();

            if (mac != null)
            {
                return await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.AddMAC(Session.Token, new UIMediaAccessControlModel()
                {
                    OperationsRecorder = Session.FocusDevices,
                    Content = mac,
                    WorkingShifts = workingShift,
                });
            }

            return false;
        }  

        public async Task<UIMediaAccessControlModel> GetMAC(Guid operationRecorderId)
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.GetLastMAC(Session.Token, operationRecorderId)).ToUIMediaAccessControl();
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Невдалося отримати MAC");
                return new UIMediaAccessControlModel();
            }
        } 
    }
}
