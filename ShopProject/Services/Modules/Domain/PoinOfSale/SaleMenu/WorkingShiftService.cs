using ShopProject.Model.Domain.Operation; 
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.FiscalServerApi;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Modules.Common; 
using ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface;
using ShopProject.Services.Modules.Mapping.Operation;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System; 
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu
{
    internal class WorkingShiftService : IWorkingShiftService
    {
        private IPrintingFiscalCheckService _printingFiscalCheckService; 
        private IWorkingShfitOperationService _workingShfitOperationService;

        private ISessionService _sessionService; 
        private ISettingService _settingService;
        private IMainWebServerService _mainWebServerService;
        public WorkingShiftService(ISessionService sessionService , IPrintingFiscalCheckService printingFiscalCheckService,
            ISettingService settingService , IMainWebServerService mainWebServerService,IWorkingShfitOperationService workingShfitOperationService)
        {
            _sessionService = sessionService; 
            _settingService = settingService;
            _printingFiscalCheckService = printingFiscalCheckService;
            _mainWebServerService = mainWebServerService;
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

        public async Task<OperationResult<bool>> OpenShift()
        {
            try
            {
                var result = new OperationResult<bool>();
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

                    result = await _workingShfitOperationService.OpenShift(shift);
                }
                else
                {
                    result.Source = response.Source;
                    result.Status = response.Status;
                    result.ErrorMessage = response.ErrorMessage;
                    result.ErrorType = response.ErrorType;
                    result.ValidationErrors = response.ValidationErrors;
                }
                return result;


            }
            catch (Exception ex) 
            {
                return new OperationResult<bool>() { ErrorMessage = ex.Message, Status = Common.Enum.ResultStatus.Error };
            }
        }

        public async Task<OperationResult<bool>> CloseShift()
        {
            try
            {
                _sessionService.CheckAndLoadWorkingShiftStatus();

                var result = new OperationResult<bool>();
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
                     
                    result = await _workingShfitOperationService.CloseShift(shift);
                }
                else
                {
                    result.Source = response.Source;
                    result.Status = response.Status;
                    result.ErrorMessage = response.ErrorMessage;
                    result.ErrorType = response.ErrorType;
                    result.ValidationErrors = response.ValidationErrors;
                }
                return result;


            }
            catch (Exception ex)
            {
                return new OperationResult<bool>() { ErrorMessage = ex.Message, Status = Common.Enum.ResultStatus.Error };
            }
        }

        public async Task<OperationInfo> GetOperationInfo(int id)
        {
            try
            {
                var result = await _mainWebServerService.DataBase.OperationController.GetOperationsInfo(id);
                return result.ToOperationInfo();
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

        public async Task<OperationResult<bool>> DepositAndWithdrawalMoney(decimal cash,TypeOperation typeOperation)
        {
            try
            {
                var result = new OperationResult<bool>();
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

                    result = await _workingShfitOperationService.DepositAndWithdrawalMoney(_sessionService.WorkingShiftStatus.WorkingShift, operation);
                     
                }
                else
                {
                    result.Source = response.Source;
                    result.Status = response.Status;
                    result.ErrorMessage = response.ErrorMessage;
                    result.ErrorType = response.ErrorType;
                    result.ValidationErrors = response.ValidationErrors;
                } 
                return result; 
            }
            catch(Exception ex)
            {
                return new OperationResult<bool>() { ErrorMessage = ex.Message, Status = Common.Enum.ResultStatus.Error };
            }
        }

        public Operation GetOperationSession()
        {
            return _sessionService.Operation;
        } 


        //public async Task PrintLastCheck()
        //{
        //    try
        //    {
        //        var items = await _mainWebServerService.DataBase.OperationController.GetOperationsІnformation(_token, _sessionService.WorkingShiftStatus.WorkingShift.ID);
        //        FiscalCheck fiscalCheck = new FiscalCheck();

        //        var operation = items.Operation.ToOperation();
        //        if (items.Discount != null)
        //        {
        //            operation.Discount = items.Discount.ToDicount();
        //        }
        //        fiscalCheck.CreateFisckalCheck(items.Products.ToProduct(_sessionService.ProductCodesUKTZED,_sessionService.ProductUnits).ToList(), operation, _sessionService.User, _sessionService.WorkingShiftStatus.OperationRecorder, _sessionService.WorkingShiftStatus.ObjectOwner);
        //        _printingFiscalCheckService.PrintCheck(fiscalCheck.GetCheck());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //} 
    }
}
