using DocumentFormat.OpenXml.Spreadsheet;
using ShopProject.Model.Domain.Operation; 
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Model.Enum; 
using ShopProject.Services.Integration.Network.WebServerApi.Interface; 
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Integration.PrintingService;
using ShopProject.Services.Modules.Common; 
using ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface;
using ShopProject.Services.Modules.Mapping.Discount;
using ShopProject.Services.Modules.Mapping.OperaionResult;
using ShopProject.Services.Modules.Mapping.Operation;
using ShopProject.Services.Modules.Mapping.Product;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu
{
    internal class WorkingShiftService : IWorkingShiftService
    { 
        private IWorkingShfitOperationService _workingShfitOperationService;

        private ISessionService _sessionService; 
        private ISettingService _settingService;
        private IMainWebServerService _mainWebServerService;
        private IPrintingFiscalCheckService _printingFiscalCheckService;
        public WorkingShiftService(ISessionService sessionService , IPrintingFiscalCheckService printingFiscalCheckService,
            ISettingService settingService , IMainWebServerService mainWebServerService,IWorkingShfitOperationService workingShfitOperationService)
        {
            _sessionService = sessionService; 
            _settingService = settingService; 
            _mainWebServerService = mainWebServerService;
            _printingFiscalCheckService = printingFiscalCheckService;
            _workingShfitOperationService = workingShfitOperationService; 
        }

        public void SetWorkingShiftStatusOnSession(WorkingShiftStatus item)
        {
            _sessionService.WorkingShiftStatus = item;
        }
        public WorkingShiftStatus GetWorkingShiftStatusFromSession()
        {
            if (_sessionService.WorkingShiftStatus.WorkingShift == null)
            {
                var setting = GetWorkingShiftStatusFromSetting();
                if(_sessionService.WorkingShiftStatus.OperationRecorder?.FiscalNumber == setting?.OperationRecorder?.FiscalNumber)
                {
                    _sessionService.WorkingShiftStatus = GetWorkingShiftStatusFromSetting();
                }
            }
            return _sessionService.WorkingShiftStatus;
        }

        public void SetWorkingShiftStatusOnSetting(WorkingShiftStatus item)
        {
            _settingService.SetSetting<WorkingShiftStatus>(item);
        }
        public WorkingShiftStatus GetWorkingShiftStatusFromSetting()
        {
            return _settingService.GetSetting<WorkingShiftStatus>();
        }

        public async Task<OperationResult<string>> OpenShift()
        {
            try
            { 
                var operationRecorder = _sessionService.WorkingShiftStatus.OperationRecorder;

                var response = await _workingShfitOperationService.GetWorkingShiftResourse(operationRecorder.FiscalNumber);

                if (response.IsSuccess)
                {
                    var shift = new WorkingShift()
                    {
                        TypeRRO = 0,
                        FiscalNumberRRO = operationRecorder.FiscalNumber,
                        TypeShiftCrateAt = ShopProject.Model.Enum.TypeWorkingShift.OpenShift,
                        UserOpenShift = _sessionService.User,
                        DataPacketIdentifier = decimal.Parse(operationRecorder.FiscalNumber),
                        FactoryNumberRRO = "v1", 
                        CreateAt = DateTimeOffset.Now,
                    };
                    if(response.Data == null)
                    {
                        shift.MACCreateAt = new ShopProject.Model.Domain.MediaAccessControl.MediaAccessControl();
                    }
                    else
                    {
                        shift.MACCreateAt = response.Data.MediaAccessControl;
                    }

                    return await _workingShfitOperationService.OpenShift(shift);
                } 
                return OperationResult<string>.Fail("Невдалося виконати операцію");


            }
            catch (Exception ex) 
            {
                return OperationResult<string>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<string>> CloseShift()
        {
            try
            {
                _sessionService.CheckAndLoadWorkingShiftStatus();
                 
                var operationRecorder = _sessionService.WorkingShiftStatus.OperationRecorder;

                var response = await _workingShfitOperationService.GetWorkingShiftResourse(operationRecorder.FiscalNumber);
                 

                if (response.IsSuccess)
                {
                    var shift = _sessionService.WorkingShiftStatus.WorkingShift; 
                    var info = await this.GetOperationInfo(shift.ID);
                    shift.TotalCheckForShift = info.TotalCheck;
                    shift.TotalReturnCheckForShift = info.TotalReturnCheck;
                    shift.UserCloseShift = _sessionService.User;
                    shift.AmountOfOfficialFundsIssuedCash = info.AmountOfOfficialFundsIssued;
                    shift.AmountOfFundsIssued = info.AmountOfFundsIssued;
                    shift.AmountOfOfficialFundsReceivedCash = info.AmountOfOfficialFundsReceived;
                    shift.AmountOfFundsReceived = info.AmountOfFundsReceived;
                    shift.AmountOfOfficialFundsIssuedCard = 0;
                    shift.AmountOfOfficialFundsReceivedCard = 0;
                    shift.EndAt = DateTimeOffset.Now;
                    shift.MACEndAt = response.Data.MediaAccessControl;
                    shift.TypeShiftEndAt = ShopProject.Model.Enum.TypeWorkingShift.CloseShift;
                     
                    return await _workingShfitOperationService.CloseShift(shift);
                } 
                return OperationResult<string>.Fail("Невдалося виконати операцію");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.Fail(ex.Message);
            }
        }

        public async Task<OperationInfo> GetOperationInfo(int id)
        {
            try
            {
                var result = await _mainWebServerService.DataBase.OperationController.GetOperationsInfo(id);
                return result.Data.ToOperationInfo();
            }
            catch (Exception ex)
            {
                throw;
            }
        }  

        public bool IsTestMode()
        {
            return _settingService.GetSetting<OperationRecorderSetting>().IsTestMode;
        }

        public async Task<OperationResult<string>> DepositAndWithdrawalMoney(decimal cash,TypeOperation typeOperation)
        {
            try
            { 
                _sessionService.CheckAndLoadWorkingShiftStatus();
                var operationRecorder = _sessionService.WorkingShiftStatus.OperationRecorder;
                var response = await _workingShfitOperationService.GetWorkingShiftResourse(operationRecorder.FiscalNumber);
                if (response.IsSuccess)
                {
                    var operation = new Operation
                    {
                        TypeOperation = typeOperation,
                        MAC = response.Data.MediaAccessControl,
                        CreatedAt = DateTime.Now,
                        NumberPayment = response.Data.OperationNumber,
                        TypePayment = TypePayment.Cash,
                        TotalPayment = cash,
                        GoodsTax = 0.ToString(),
                    };

                    return await _workingShfitOperationService.DepositAndWithdrawalMoney(_sessionService.WorkingShiftStatus.WorkingShift, operation);
                     
                }
                return OperationResult<string>.Fail("Невдалося виконати операцію");
            }
            catch(Exception ex)
            {
                return OperationResult<string>.Fail(ex.Message);
            }
        }

        public Operation GetOperationSession()
        {
            return _sessionService.Operation;
        }


        public async Task<OperationResult<bool>> PrintLastCheck()
        {
            try
            {
                var result = (await _mainWebServerService.DataBase.OperationController.GetLastNumberOperation(_sessionService.WorkingShiftStatus.WorkingShift.ID)).ToOperationResult();
                if (result.IsSuccess)
                {
                    FiscalCheck fiscalCheck = new FiscalCheck();

                    var operation = result.Data.Operation.ToOperation();
                    if (result.Data.Discount != null)
                    {
                        operation.Discount = result.Data.Discount.ToDicount();
                    }
                    fiscalCheck.CreateFisckalCheck(result.Data.Products.ToProduct(_sessionService.ProductCodesUKTZED, _sessionService.ProductUnits).ToList(), operation, _sessionService.User, _sessionService.WorkingShiftStatus.OperationRecorder, _sessionService.WorkingShiftStatus.TaxObject);
                    _printingFiscalCheckService.PrintCheck(fiscalCheck.GetCheck());

                    return OperationResult<bool>.Success(true);
                }
                else
                {
                    return OperationResult<bool>.Fail("невдлося виконати операцію");
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message);
            }
        }
    }
}
