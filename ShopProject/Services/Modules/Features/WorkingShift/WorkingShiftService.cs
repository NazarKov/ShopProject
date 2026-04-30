using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Services.Integration.File.Xml;
using ShopProject.Services.Integration.Network.FiscalServerApi;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Integration.Network.WebServerApi.Mapping;
using ShopProject.Services.Integration.Printing;
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Integration.PrintingService;
using ShopProject.Services.Modules.Model.WorkingShift.Interface;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopProject.Services.Modules.Model.WorkingShift
{
    internal class WorkingShiftService : IWorkingShiftService
    {
        private IPrintingFiscalCheckService _printingFiscalCheckService;
        private MainFiscalServerController _fiscalOperationController;


        private readonly string _token;
        private ISessionService _sessionService; 
        private ISettingService _settingService;
        private IMainWebServerService _mainWebServerService;
        public WorkingShiftService(ISessionService sessionService , IPrintingFiscalCheckService printingFiscalCheckService,ISettingService settingService , IMainWebServerService mainWebServerService)
        {
            _sessionService = sessionService;
            _token = _sessionService.User.Token; 
            _settingService = settingService;
            _printingFiscalCheckService = printingFiscalCheckService;
            _mainWebServerService = mainWebServerService;
            _fiscalOperationController = new MainFiscalServerController(); 
        }

        public async Task<bool> OpenShift(ShopProject.Model.Domain.WorkingShift.WorkingShift shiftEntity)
        {
            try
            {
                if (_fiscalOperationController.OpenShift(shiftEntity) == "OK")
                { 

                    _sessionService.WorkingShiftStatus.WorkingShift = shiftEntity;
                    shiftEntity.MACCreateAt = CreateMac(shiftEntity);
                    _sessionService.WorkingShiftStatus.WorkingShift.ID = await SaveDataBaseOpenShift(shiftEntity); 
                    _settingService.SetSetting<WorkingShiftStatus>(_sessionService.WorkingShiftStatus);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }

        public void AddKey(SignatureKey key) => _fiscalOperationController.AddKey(key);

        public async Task<bool> CloseShift(ShopProject.Model.Domain.WorkingShift.WorkingShift shiftEntity)
        {
            if (_fiscalOperationController.CloseShift(shiftEntity) == "OK")
            {
                shiftEntity.ID = _sessionService.WorkingShiftStatus.WorkingShift.ID;
                shiftEntity.MACEndAt = CreateMac(shiftEntity);
                return await SaveDataBaseCloseShift(shiftEntity);
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DepositAndWithdrawalMoney(ShopProject.Model.Domain.WorkingShift.WorkingShift shift, Operation operation)
        {
            if (_fiscalOperationController.DepositAndWithdrawalMoney(shift, operation) == "OK")
            {
                operation.Shift = _sessionService.WorkingShiftStatus.WorkingShift;
                operation.MAC = CreateMac(shift);
                await SaveDataBaseOperation(operation);
                return true;
            }
            return false;
        }


        private async Task<int> SaveDataBaseOperation(Operation operation)
        {
            try
            {
                return await _mainWebServerService.DataBase.OperationController.AddOperation(_token, operation);
            }
            catch
            {
                return 0;
            }
        }
        private async Task<int> SaveDataBaseOpenShift(ShopProject.Model.Domain.WorkingShift.WorkingShift shift)
        {
            try
            {
                return await _mainWebServerService.DataBase.WorkingShiftContoller.AddWorkingShift(_token, shift);
            }
            catch
            {
                return 0;
            }
        }
        private async Task<bool> SaveDataBaseCloseShift(ShopProject.Model.Domain.WorkingShift.WorkingShift shift)
        {
            try
            {
                return await _mainWebServerService.DataBase.WorkingShiftContoller.UpdateWorkingShift(_token, shift);
            }
            catch
            {
                return false;
            }
        }

        private MediaAccessControl CreateMac(ShopProject.Model.Domain.WorkingShift.WorkingShift workingShift, Operation operation = null)
        {
            return new MediaAccessControl()
            {
                OperationsRecorder = _sessionService.WorkingShiftStatus.OperationRecorder,
                Content = XmlServise.GenerationMACForXML(),
                WorkingShifts = workingShift,
                Operation = operation
            };
        }

        public async Task<MediaAccessControl> GetMAC(Guid operationRecorderId)
        {
            try
            {
                var mac = _sessionService.WorkingShiftStatus.MediaAccessControl;
                if (mac != null && mac.Content != string.Empty)
                {
                    return mac;
                }
                else
                {
                    return (await _mainWebServerService.DataBase.MediaAccessControlController.GetLastMAC(_token, operationRecorderId)).ToMediaAccessControl();
                }

            }
            catch
            { 
                return new MediaAccessControl();
            }
        }

        public async Task<ShopProject.Model.Domain.WorkingShift.WorkingShift> GetWorkingShift(string id)
        {
            try
            {
                var result = await _mainWebServerService.DataBase.WorkingShiftContoller.GetWorkingShift(_token, id);
                return result.ToWorkingShift();
            }
            catch (Exception ex)
            {
                throw; 
            }
        }
        public async Task<OperationInfo> GetOperationInfo(int id)
        {
            try
            {
                var result = await _mainWebServerService.DataBase.OperationController.GetOperationsInfo(_token, id);
                return result.ToOperationInfo();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> GetLocalNumber()
        {
            try
            {
                var item = await _mainWebServerService.DataBase.OperationController.GetLastNumberOperation(_token, _sessionService.WorkingShiftStatus.WorkingShift.ID);

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

        public async Task PrintLastCheck()
        {
            try
            {
                var items = await _mainWebServerService.DataBase.OperationController.GetOperationsІnformation(_token, _sessionService.WorkingShiftStatus.WorkingShift.ID);
                FiscalCheck fiscalCheck = new FiscalCheck();

                var operation = items.Operation.ToOperation();
                if (items.Discount != null)
                {
                    operation.Discount = items.Discount.ToDicount();
                }
                fiscalCheck.CreateFisckalCheck(items.Products.ToProduct(_sessionService.ProductCodesUKTZED,_sessionService.ProductUnits).ToList(), operation, _sessionService.User, _sessionService.WorkingShiftStatus.OperationRecorder, _sessionService.WorkingShiftStatus.ObjectOwner);
                _printingFiscalCheckService.PrintCheck(fiscalCheck.GetCheck());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void LoadSaleMenuDataFromFile()
        {
            var workingshiftStatus = _settingService.GetSetting<WorkingShiftStatus>();
            if (workingshiftStatus == null)
            {
                workingshiftStatus = new WorkingShiftStatus();
            }
            else
            {
                if (_sessionService.WorkingShiftStatus != null)
                {
                    if (_sessionService.WorkingShiftStatus.OperationRecorder == null)
                    {
                        _sessionService.WorkingShiftStatus.OperationRecorder = workingshiftStatus.OperationRecorder;
                    }
                    if (_sessionService.WorkingShiftStatus.WorkingShift == null)
                    {
                        _sessionService.WorkingShiftStatus.WorkingShift = workingshiftStatus.WorkingShift;
                    }
                    else
                    {
                        if (_sessionService.WorkingShiftStatus.WorkingShift.UserOpenShift == null)
                        {
                            if (workingshiftStatus.WorkingShift != null && workingshiftStatus.WorkingShift.UserOpenShift != null)
                            {
                                _sessionService.WorkingShiftStatus.WorkingShift.UserOpenShift = workingshiftStatus.WorkingShift.UserOpenShift;
                            }
                        }
                    }

                    _sessionService.WorkingShiftStatus.StatusShift = workingshiftStatus.StatusShift;
                    _sessionService.WorkingShiftStatus.StatusOnline = workingshiftStatus.StatusOnline;
                    _sessionService.WorkingShiftStatus.MediaAccessControl = workingshiftStatus.MediaAccessControl;
                    _sessionService.WorkingShiftStatus.ObjectOwner = workingshiftStatus.ObjectOwner;
                }
            }
        }
        public void SetWorkingShiftStatusOnSession(ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus item)
        {
            _sessionService.WorkingShiftStatus = item;
        }
        public ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus GetWorkingShiftStatusFromSession()
        {
            return _sessionService.WorkingShiftStatus;
        }

        public void SetWorkingShiftStatusOnSetting(ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus item)
        {
            _settingService.SetSetting<WorkingShiftStatus>(item);
        }
        public ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus GetWorkingShiftStatusFromSetting()
        {
            return _settingService.GetSetting<WorkingShiftStatus>();
        }

        public void SetTabsOnSession(ObservableCollection<TabItem> items)
        {
            _sessionService.Tabs = items;
        }
        public ObservableCollection<TabItem> GetTabsFromSession()
        {
            return _sessionService.Tabs;
        }
    }
}
