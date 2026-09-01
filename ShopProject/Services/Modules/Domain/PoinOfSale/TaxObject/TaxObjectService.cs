using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.TaxObjectUser;
using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Model.Enum; 
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi;
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Common.Enum;
using ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject.Interface;
using ShopProject.Services.Modules.Mapping.OperationRecorder;
using ShopProject.Services.Modules.Mapping.TaxObject;
using ShopProject.Services.Modules.Mapping.TaxObjectUser;
using ShopProject.Services.Modules.Mapping.User;
using ShopProject.Services.Modules.Mapping.UserRole;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxObjectModel = ShopProject.Model.Domain.TaxObject.TaxObject;

namespace ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject
{
    internal class TaxObjectService : ITaxObjectService
    {
        private SigningFileContoller _signingFileController;
        private MainElectronicTaxAccountController _accountController; 

        private IMainWebServerService _webServerService;
        private ISessionService _sessionService;
        private ISettingService _settingService;

        public TaxObjectService(IMainWebServerService mainWebServerService,ISessionService sessionService,ISettingService settingService)
        {
            _webServerService = mainWebServerService;
            _sessionService = sessionService;
            _settingService= settingService;

            _signingFileController = new SigningFileContoller();
            _accountController = new MainElectronicTaxAccountController();
            _signingFileController.Initialize(false); 
        }




        public async Task<OperationResult<Paginator<TaxObjectModel, TypeStatusTaxObject>>> GetPageColumn(int page, int countColumn, TypeStatusTaxObject status)
        {
            var result = new OperationResult<Paginator<TaxObjectModel, TypeStatusTaxObject>>();

            var response = await _webServerService.DataBase.TaxObjectController.GetPageColumn(new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<TaxObjectModel, TypeStatusTaxObject>()
                    {
                        Data = paginator.Data.ToTaxObject(),
                        DataType = (TypeStatusTaxObject)paginator.DataType,
                        Page = page,
                        Pages = paginator.Pages,
                    };


                }
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        }

        public async Task<OperationResult<Paginator<TaxObjectModel, TypeStatusTaxObject>>> SearchByName(string item, int page, int countColumn, TypeStatusTaxObject status)
        {
            var result = new OperationResult<Paginator<TaxObjectModel, TypeStatusTaxObject>>();

            var response = await _webServerService.DataBase.TaxObjectController.GetByNamePageColumn(item, new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<TaxObjectModel, TypeStatusTaxObject>()
                    {
                        Data = paginator.Data.ToTaxObject(),
                        DataType = (TypeStatusTaxObject)paginator.DataType,
                        Page = page,
                        Pages = paginator.Pages,
                    };


                }
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        }

        public async Task<OperationResult<TaxObjectModel>> Add(TaxObjectModel taxObject)
        {
            taxObject.TypeStatus = TypeStatusTaxObject.Open;
            var result = new OperationResult<TaxObjectModel>();
            result.Data = taxObject;
            Validation(result);
            if (result.IsError)
            {
                return result;
            }
            if (result.IsSuccess)
            {
                var response = await _webServerService.DataBase.TaxObjectController.Add(result.Data.ToCreateTaxObject());

                if (response.Data != null)
                {
                    result.Data = response.Data.ToTaxObject();
                }
                result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                result.ErrorMessage = response.Error;
                result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                result.ValidationErrors = response.Errors;

                return result;
            }

            return new OperationResult<TaxObjectModel>()
            {
                ErrorMessage = "Невдалося викоанти операцію",
                Status = ResultStatus.Error,
            };
        }

        public async Task<OperationResult<IEnumerable<TaxObjectModel>>> AddRange(IEnumerable<TaxObjectModel> taxObjects)
        {
            var result = new OperationResult<IEnumerable<TaxObjectModel>>();
            foreach (var taxObject in taxObjects) 
            {
                taxObject.LoadTaxServer = true;
            }
            result.Data = taxObjects;
            var response = await _webServerService.DataBase.TaxObjectController.AddRange(result.Data.ToCreateTaxObject());

            if (response.Data != null)
            {
                result.Data = response.Data.ToTaxObject();
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result; 
        }

        private OperationResult<TaxObjectModel> Validation(OperationResult<TaxObjectModel> item)
        {
            if (item.Data == null)
            {
                item.ErrorMessage = "Заповніть всі поля";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }


            if (item.Data.NameOwner == string.Empty)
            {
                item.ErrorMessage = "Ведіть Власник обєкта";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.NameObject == string.Empty)
            {
                item.ErrorMessage = "Ведіть назву обєкта";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.Address == string.Empty)
            {
                item.ErrorMessage = "Ведіть адресу обєкта";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            item.Status = ResultStatus.Success;
            return item;
        }


        public async Task<OperationResult<IEnumerable<TaxObjectModel>>> GetTaxServer(string pathFile, string passwordKey)
        {
            try
            { 
                var result = Validation(pathFile,passwordKey);
                if (result.IsError)
                {
                    return result;
                }

                var taxObjects = new List<TaxObjectModel>();
                if (_signingFileController.GetDataToFile(pathFile, passwordKey))
                {

                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _accountController.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;

                    foreach (var item in infoUser.ElementAt(8).listValues)
                    {

                        var taxobject = new TaxObjectModel()
                        {
                            NameOwner = nameUser,
                            NameObject = item.NAME,
                            Address = item.ADDRESS,
                            C_DISTR = item.C_DISTR,
                            TypeOfRights = item.TYPE_OF_RIGHTS,
                            KATOTTG = item.KATOTTG,
                            CodeObject = item.TO_CODE.ToString(),

                        };
                        if (taxobject.C_TERRIT != null)
                        {
                            taxobject.C_TERRIT = item.C_TERRIT.ToString();
                        }
                        if (taxobject.REG_NUM_OBJ != null)
                        {
                            taxobject.REG_NUM_OBJ = item.REG_NUM_OBJ.ToString();
                        }
                        if (taxobject.TypeObjectName != null)
                        {
                            taxobject.TypeObjectName = item.TYPE_OF_RIGHTS.ToString();
                        }


                        if (item.STAN_OBJECT == "Об'єкт відчужено / повернено власнику")
                        {
                            taxobject.TypeStatus = TypeStatusTaxObject.Closed;
                        }
                        else if (item.STAN_OBJECT == "орендується")
                        {
                            taxobject.TypeStatus = TypeStatusTaxObject.Open;
                        }
                        taxobject.Status = item.STAN_OBJECT;

                        var time = item.D_ACC_START;
                        if (time != null)
                        {
                            taxobject.D_ACC_START = DateTime.Parse(item.D_ACC_START);
                        }
                        time = item.D_LAST_CH;
                        if (time != null)
                        {
                            taxobject.D_LAST_CH = DateTime.Parse(item.D_LAST_CH);
                        }
                        time = item.D_ACC_END;
                        if (time != null)
                        {
                            taxobject.D_ACC_END = DateTime.Parse(item.D_ACC_END);
                        } 

                        taxObjects.Add(taxobject);
                    }
                    result.Data = taxObjects;
                    result.Status = ResultStatus.Success;
                    return result; 
                }
                return new OperationResult<IEnumerable<TaxObjectModel>>()
                {
                    ErrorMessage = "Невдалося викоанти операцію",
                    Status = ResultStatus.Error,
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<TaxObjectModel>>()
                {
                    ErrorMessage = ex.Message,
                    Status = ResultStatus.Error,
                };
            } 
        } 
        private OperationResult<IEnumerable<TaxObjectModel>> Validation(string pahtKey,string passwordKey)
        {
            if (string.IsNullOrEmpty(passwordKey))
            {
                return new OperationResult<IEnumerable<TaxObjectModel>>()
                {
                    ErrorMessage = "Ведіть пароль ключа",
                    Status = ResultStatus.Error,
                    ErrorType = ErrorType.Validation,
                };
            }
            if (string.IsNullOrEmpty(pahtKey))
            {
                return new OperationResult<IEnumerable<TaxObjectModel>>()
                {
                    ErrorMessage = "Виберіть ключ",
                    Status = ResultStatus.Error,
                    ErrorType = ErrorType.Validation,
                };
            } 
            return new OperationResult<IEnumerable<TaxObjectModel>>()
            { 
                Status = ResultStatus.Success,
            };
        }

        public void SetBindingTaxObjectTOSession(TaxObjectModel taxObject)
        {
            _sessionService.BindingTaxObject = taxObject;
        }
        public TaxObjectModel GetBindingTaxObjectOnSession()
        {
            return _sessionService.BindingTaxObject;
        }

        public async Task<OperationResult<bool>> AddBindingOperationRecorderToTaxObject(Guid idTaxObject , IEnumerable<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> operationRecorders)
        {
            var result = new OperationResult<bool>(); 
            var response = await _webServerService.DataBase.TaxObjectController.AddBindingOperationRecorder(idTaxObject.ToString(),operationRecorders.ToOperationRecorderDto());

            result.Data = response.Data;
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        }

        public async Task<OperationResult<bool>> AddBindingUserToTaxObject(Guid idTaxObject, IEnumerable<ShopProject.Model.Domain.User.User> users)
        { 
            var result = new OperationResult<bool>();
            var response = await _webServerService.DataBase.TaxObjectController.AddBindingUser(idTaxObject.ToString(), users.ToUserDto(_sessionService.Roles.ToUserRoleDto()));

            result.Data = response.Data;
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        }

        public async Task<OperationResult<IEnumerable<TaxObjectUser>>> GetTaxObjectsAssignedUser()
        {
            var result = new OperationResult<IEnumerable<TaxObjectUser>>();
            var response = await _webServerService.DataBase.TaxObjectController.GetTaxObjectsAssignedUser(_sessionService.User.ID.ToString());

            result.Data = response.Data.ToTaxObjectUser(_sessionService.Roles);
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        } 

        public void SetPoinOfSaleOnSession(TaxObjectModel taxObject, ShopProject.Model.Domain.OperationRecorder.OperationRecorder operationRecorder)
        {
            var workingShift = new WorkingShift();
            if (_sessionService.WorkingShiftStatus != null)
            {
                workingShift = _sessionService.WorkingShiftStatus.WorkingShift;
            }
            _sessionService.WorkingShiftStatus = new ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus()
            { 
                TaxObject = taxObject,
                OperationRecorder = operationRecorder, 
            };
            if (workingShift != null)
            {
                _sessionService.WorkingShiftStatus.WorkingShift = workingShift;
            }
        }

        public async Task<OperationResult<bool>> UpdateParameter(string parameter, object value, TaxObjectModel item)
        {
            var result = new OperationResult<bool>();
            var response = await _webServerService.DataBase.TaxObjectController.UpdateParameter(parameter, value, item.ID.ToString());

            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;
            return result;
        }

        public async Task<OperationResult<TaxObjectModel>> Update(TaxObjectModel taxObject)
        {
            taxObject.TypeStatus = TypeStatusTaxObject.Open;
            var result = new OperationResult<TaxObjectModel>();
            result.Data = taxObject;
            Validation(result);
            if (result.IsError)
            {
                return result;
            }
            if (result.IsSuccess)
            {
                var response = await _webServerService.DataBase.TaxObjectController.Update(result.Data.ToUpdateTaxObjectDto());
                 
                result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                result.ErrorMessage = response.Error;
                result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                result.ValidationErrors = response.Errors;

                return result;
            }

            return new OperationResult<TaxObjectModel>()
            {
                ErrorMessage = "Невдалося викоанти операцію",
                Status = ResultStatus.Error,
            };
        }
    }
}
