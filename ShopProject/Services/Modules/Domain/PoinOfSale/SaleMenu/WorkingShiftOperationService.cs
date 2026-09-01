using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.Operation; 
using ShopProject.Model.Domain.WorkingShift; 
using ShopProject.Services.Integration.File.Xml;
using ShopProject.Services.Integration.Network.FiscalServerApi;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Common.Enum;
using ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface;
using ShopProject.Services.Modules.Mapping.Operation;
using ShopProject.Services.Modules.Mapping.WorkingShift;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using System; 
using System.Threading.Tasks; 
using WorkingShiftModel = ShopProject.Model.Domain.WorkingShift.WorkingShift;
using ProductModel = ShopProject.Model.Domain.Product.Product;
using OrderModel = ShopProject.Model.Domain.Order.Order; 
using System.Collections.Generic;
using ShopProject.Services.Modules.Mapping.Discount;
using System.Linq;

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
            _fiscalOperationController = new MainFiscalServerController();
            _mainWebServerService = mainWebServerService;
            _settingService = settingService;
            _fiscalOperationController.AddKey(_sessionService.User.SignatureKey); 
        } 

        public async Task<OperationResult<bool>> OpenShift(WorkingShiftModel shift)
        {
            try
            { 
                var result = new OperationResult<bool>();
                var id = _fiscalOperationController.OpenShift(shift, (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                if (!string.IsNullOrEmpty(id))
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

                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>() { ErrorMessage = ex.Message, Status = Common.Enum.ResultStatus.Error };
            }
        }
        public async Task<OperationResult<bool>> CloseShift(WorkingShiftModel shift)
        {
            try
            {
                var result = new OperationResult<bool>();
                var id = _fiscalOperationController.CloseShift(shift, (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                if (!string.IsNullOrEmpty(id))
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
                return result;
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>() { ErrorMessage = ex.Message, Status = Common.Enum.ResultStatus.Error };
            }
        }

        public async Task<OperationResult<bool>> DepositAndWithdrawalMoney(WorkingShiftModel shift, Operation operation)
        {
            try
            {
                var result = new OperationResult<bool>();
                var id = _fiscalOperationController.DepositAndWithdrawalMoney(shift, operation, (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
                if (!string.IsNullOrEmpty(id))
                {
                    operation.FiscalServerId = id;
                    operation.MAC = CreateMac(shift); 
                    operation.Shift = shift; 

                    var response =  await _mainWebServerService.DataBase.OperationController.Add(operation.ToCreateOperationDto());
                    result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                    result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                    result.ErrorMessage = response.Error;
                    result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                    result.ValidationErrors = response.Errors; 
                }

                return result;
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>() { ErrorMessage = ex.Message, Status = Common.Enum.ResultStatus.Error };
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




        public async Task<OperationResult<bool>> SendCheck(IEnumerable<ProductModel> products, Operation operation)
        { 
            var result = new OperationResult<bool>();
            var workingShift = _sessionService.WorkingShiftStatus.WorkingShift;

            if (workingShift == null)
            {
                return OperationResult<bool>.Fail("Невдалося завантажити зміну");
            }


            var id = _fiscalOperationController.SendReturnFiscalCheck(workingShift, operation, products.ToList(), (_settingService.GetSetting<ShopProject.Model.Domain.Setting.OperationRecorderSetting>()).IsTestMode);
            if (id != string.Empty)
            {
                operation.Shift = workingShift;
                operation.FiscalServerId = id;
                operation.MAC = CreateMac(workingShift,operation);
                result = await SaveDataBase(operation, products);
                _sessionService.Operation = operation;
            }
            return result;
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
                throw;
            }
        }
    }
}
