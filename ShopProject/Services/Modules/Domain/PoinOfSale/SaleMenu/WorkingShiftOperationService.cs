using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.Operation; 
using ShopProject.Model.Domain.WorkingShift; 
using ShopProject.Services.Integration.File.Xml;
using ShopProject.Services.Integration.Network.FiscalServerApi;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Common.Enum;
using ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface;
using ShopProject.Services.Modules.Mapping.Discount;
using ShopProject.Services.Modules.Mapping.Operation;
using ShopProject.Services.Modules.Mapping.WorkingShift;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using OrderModel = ShopProject.Model.Domain.Order.Order; 
using ProductModel = ShopProject.Model.Domain.Product.Product;
using WorkingShiftModel = ShopProject.Model.Domain.WorkingShift.WorkingShift;

namespace ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu
{
    internal class WorkingShiftOperationService : IWorkingShfitOperationService
    {
        public ISessionService _sessionService;
        private MainFiscalServerController _fiscalOperationController;
        private IMainWebServerService _mainWebServerService;
        private ISettingService _settingService; 
        public WorkingShiftOperationService(ISessionService sessionService  ,IMainWebServerService mainWebServerService , ISettingService settingService)
        {
            _sessionService = sessionService; 
            _mainWebServerService = mainWebServerService;
            _settingService = settingService; 
            _fiscalOperationController = new MainFiscalServerController(_sessionService.User.SignatureKey);
        } 

        public async Task<OperationResult<string>> OpenShift(WorkingShiftModel shift)
        {
            try
            { 
                var result = _fiscalOperationController.OpenShift(shift, (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                if(result.IsError || result.ErrorType == ErrorType.ErrorKey)
                {
                    var key = _sessionService.User.SignatureKey;
                    if(key!=null && key.Signature!=null && key.SignaturePassword != null)
                    {
                        _fiscalOperationController.AddKey(_sessionService.User.SignatureKey);
                        result = _fiscalOperationController.OpenShift(shift, (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                    }
                    else
                    {
                        return result;
                    } 
                }

                if (result.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(result.Data))
                    {
                        shift.MACCreateAt = CreateMac(shift);
                        var response = await _mainWebServerService.DataBase.WorkingShiftContoller.AddWorkingShift(shift);

                        result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                        result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                        result.ErrorMessage = response.Error;
                        result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                        result.ValidationErrors = response.Errors;

                        if (result.IsSuccess)
                        {
                            _sessionService.WorkingShiftStatus.WorkingShift = response.Data.ToWorkingShift();
                            _sessionService.WorkingShiftStatus.OpenShiftTime = DateTime.Now;
                            _sessionService.WorkingShiftStatus.Status = ShopProject.Model.Enum.TypeStatusShift.Open;
                            _settingService.SetSetting<WorkingShiftStatus>(_sessionService.WorkingShiftStatus);
                        } 
                    }
                } 
                return result;
            }
            catch (Exception ex)
            {
                return OperationResult<string>.Fail(ex.Message);
            }
        }
        public async Task<OperationResult<string>> CloseShift(WorkingShiftModel shift)
        {
            try
            {
                var result =  _fiscalOperationController.CloseShift(shift, (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                if (result.IsError || result.ErrorType == ErrorType.ErrorKey)
                {
                    var key = _sessionService.User.SignatureKey;
                    if (key != null && key.Signature != null && key.SignaturePassword != null)
                    {
                        _fiscalOperationController.AddKey(_sessionService.User.SignatureKey);
                        result = _fiscalOperationController.CloseShift(shift, (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                    }
                    else
                    {
                        return result;
                    }
                }


                if (result.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(result.Data))
                    {
                        shift.MACEndAt = CreateMac(shift);
                        var response = await _mainWebServerService.DataBase.WorkingShiftContoller.UpdateWorkingShift(shift);
                        result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                        result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                        result.ErrorMessage = response.Error;
                        result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                        result.ValidationErrors = response.Errors;
                        if (result.IsSuccess)
                        {
                            _sessionService.WorkingShiftStatus.WorkingShift = null;
                            _sessionService.WorkingShiftStatus.OpenShiftTime = null;
                            _sessionService.WorkingShiftStatus.Status = ShopProject.Model.Enum.TypeStatusShift.Close;
                            _settingService.SetSetting<WorkingShiftStatus>(_sessionService.WorkingShiftStatus);
                        }
                    }
                } 
                return result;
            }
            catch (Exception ex)
            {
                return OperationResult<string>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<string>> DepositAndWithdrawalMoney(WorkingShiftModel shift, Operation operation)
        {
            try
            {
                var result   = _fiscalOperationController.DepositAndWithdrawalMoney(shift, operation, (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                if (result.IsError || result.ErrorType == ErrorType.ErrorKey)
                {
                    var key = _sessionService.User.SignatureKey;
                    if (key != null && key.Signature != null && key.SignaturePassword != null)
                    {
                        _fiscalOperationController.AddKey(_sessionService.User.SignatureKey);
                        result = _fiscalOperationController.DepositAndWithdrawalMoney(shift, operation, (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                    }
                    else
                    {
                        return result;
                    }
                }
                if (result.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(result.Data))
                    {
                        operation.FiscalServerId = result.Data;
                        operation.MAC = CreateMac(shift);
                        operation.Shift = shift;

                        var response = await _mainWebServerService.DataBase.OperationController.Add(operation.ToCreateOperationDto());
                        result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                        result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                        result.ErrorMessage = response.Error;
                        result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                        result.ValidationErrors = response.Errors;
                    }
                }  
                return result;
            }
            catch (Exception ex)
            {
                return OperationResult<string>.Fail(ex.Message);
            }
        } 

        private MediaAccessControl CreateMac(WorkingShiftModel workingShift, Operation? operation = null)
        {
            return new MediaAccessControl()
            {
                OperationsRecorder = _sessionService.WorkingShiftStatus.OperationRecorder,
                Content = XmlServise.GenerationMACForXML(),
                WorkingShifts = workingShift,
                Operation = operation
            };
        } 
        public async Task<OperationResult<WorkingShiftResourse>> GetWorkingShiftResourse(string fiscalNumberRRo)
        {
            var result = new OperationResult<WorkingShiftResourse>();
            var response = await _mainWebServerService.DataBase.WorkingShiftContoller.GetResourseById(fiscalNumberRRo);
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;
            if (result.IsSuccess)
            {
                var data = response.Data.ToWorkingShiftResourse();
                if (string.IsNullOrEmpty(data.OperationNumber))
                {
                    data.OperationNumber = "1";
                }
                else
                {
                    data.OperationNumber = (Convert.ToInt32(data.OperationNumber) + 1).ToString();
                }
                if(data.MediaAccessControl == null)
                {
                    data.MediaAccessControl = new();
                }
                result.Data = data;
            }
            return result; 
        }  
        public async Task<OperationResult<string>> SendCheck(IEnumerable<ProductModel> products, Operation operation)
        {
            try
            { 
                var workingShift = _sessionService.WorkingShiftStatus.WorkingShift;

                if (workingShift == null)
                {
                    return OperationResult<string>.Fail("Невдалося завантажити зміну");
                }


                var result = _fiscalOperationController.SendReturnFiscalCheck(workingShift, operation, products.ToList(), (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                if (result.IsError || result.ErrorType == ErrorType.ErrorKey)
                { 
                    var key = _sessionService.User.SignatureKey;
                    if (key != null && key.Signature != null && key.SignaturePassword != null)
                    {
                        _fiscalOperationController.AddKey(_sessionService.User.SignatureKey);
                        result = _fiscalOperationController.SendReturnFiscalCheck(workingShift, operation, products.ToList(), (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                    }
                    else
                    {
                        return result;
                    }
                }

                if (result.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(result.Data))
                    {
                        operation.Shift = workingShift;
                        operation.FiscalServerId = result.Data;
                        operation.MAC = CreateMac(workingShift, operation);
                        var resultOperation = await SaveDataBase(operation, products);

                        if (resultOperation.IsSuccess) 
                        {
                            _sessionService.Operation = operation;
                            return result;
                        } 
                    }
                } 
                return result;
            }
            catch (Exception ex) 
            {
                return OperationResult<string>.Fail(ex.Message);
            }
            
        } 
        private async Task<OperationResult<bool>> SaveDataBase(Operation operation, IEnumerable<ProductModel> products)
        {
            try
            { 
                if (operation.Discount != null)
                {
                    operation.Discount.ID = (await _mainWebServerService.DataBase.DiscountController.AddDiscount(operation.Discount.ToCreateDicount())).Data;
                }
                var result = (await _mainWebServerService.DataBase.OperationController.Add(operation.ToCreateOperationDto())).Data; 

                if (result.ID >= 0)
                {
                    List<OrderModel> orders = new List<OrderModel>();
                    foreach (ProductModel item in products)
                    {
                        orders.Add(new OrderModel()
                        {
                            Operation= new Operation() { ID = result.ID},
                            Product = item,
                            Count = (int)item.Count,

                        });
                    }
                    await _mainWebServerService.DataBase.OrderController.AddOrderRange(orders);
                }
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message);
            }
        }
    }
}
