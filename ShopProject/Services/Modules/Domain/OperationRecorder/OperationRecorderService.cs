using ShopProject.Model.Domain.Paginator; 
using ShopProject.Model.Enum;  
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi;
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Common.Enum;
using ShopProject.Services.Modules.Domain.OperationRecorder.Interface;
using ShopProject.Services.Modules.Mapping.OperationRecorder; 
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using OperationRecorderModel = ShopProject.Model.Domain.OperationRecorder.OperationRecorder;

namespace ShopProject.Services.Modules.Domain.OperationRecorder
{
    internal class OperationRecorderService : IOperationRecorderService
    {
        private SigningFileContoller _signingFileController;
        private MainElectronicTaxAccountController _mainControllerHttp; 
        private IMainWebServerService _webServerService;
        private ISessionService _sessionService; 

        public OperationRecorderService(IMainWebServerService mainWebServerService,ISessionService sessionService,ISettingService settingService)
        {
            _webServerService = mainWebServerService;
            _sessionService = sessionService; 
            _signingFileController = new SigningFileContoller();
            _mainControllerHttp = new MainElectronicTaxAccountController();
            _signingFileController.Initialize(false);
        }

        public async Task<OperationResult<OperationRecorderModel>> Add(OperationRecorderModel item)
        {
            var result = new OperationResult<OperationRecorderModel>(); 
            result.Data = item;
            Validation(result);
            if (result.IsError)
            {
                return result;
            }
            if (result.IsSuccess)
            {
                var response = await _webServerService.DataBase.OperationRecorederController.Add(result.Data.ToCreateOperationRecorderDto());

                if (response.Data != null)
                {
                    result.Data = response.Data.ToOperationRecorder();
                }
                result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                result.ErrorMessage = response.Error;
                result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                result.ValidationErrors = response.Errors;

                return result;
            }

            return new OperationResult<OperationRecorderModel>()
            {
                ErrorMessage = "Невдалося викоанти операцію",
                Status = ResultStatus.Error,
            };

        }

        public async Task<OperationResult<IEnumerable<OperationRecorderModel>>> AddRange(IEnumerable<OperationRecorderModel> items)
        {
            var result = new OperationResult<IEnumerable<OperationRecorderModel>>(); 
            result.Data = items;

            var response = await _webServerService.DataBase.OperationRecorederController.AddRange(result.Data.ToCreateOperationRecorderDto());

            if (response.Data != null)
            {
                result.Data = response.Data.ToOperationRecorder();
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;  
        }

        public async Task<OperationResult<OperationRecorderModel>> Update(OperationRecorderModel item)
        {
            var result = new OperationResult<OperationRecorderModel>();
            result.Data = item;
            Validation(result);
            if (result.IsError)
            {
                return result;
            }
            if (result.IsSuccess)
            {
                var response = await _webServerService.DataBase.OperationRecorederController.Update(result.Data.ToUpdateOperationRecorderDto());

                if (response.Data != null)
                {
                    result.Data = response.Data.ToOperationRecorder();
                }
                result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                result.ErrorMessage = response.Error;
                result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                result.ValidationErrors = response.Errors;

                return result;
            }

            return new OperationResult<OperationRecorderModel>()
            {
                ErrorMessage = "Невдалося викоанти операцію",
                Status = ResultStatus.Error,
            };
        }
        private OperationResult<OperationRecorderModel> Validation(OperationResult<OperationRecorderModel> item)
        {
            if (item.Data == null)
            {
                item.ErrorMessage = "Заповніть всі поля";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }


            if (item.Data.Name == string.Empty)
            {
                item.ErrorMessage = "Ведіть назву каси";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.FiscalNumber == string.Empty)
            {
                item.ErrorMessage = "Ведіть фіскальний номер";
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

        public async Task<OperationResult<Paginator<OperationRecorderModel, TypeStatusOperationRecorder>>> GetPageColumn(int page, int countColumn, TypeStatusOperationRecorder status)
        {
            var result = new OperationResult<Paginator<OperationRecorderModel, TypeStatusOperationRecorder>>();

            var response = await _webServerService.DataBase.OperationRecorederController.GetPageColumn(new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<OperationRecorderModel, TypeStatusOperationRecorder>()
                    {
                        Data = paginator.Data.ToOperationRecorder(),
                        DataType = (TypeStatusOperationRecorder)paginator.DataType,
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

        public async Task<OperationResult<Paginator<OperationRecorderModel, TypeStatusOperationRecorder>>> SearchByName(string item, int page, int countColumn, TypeStatusOperationRecorder status)
        {
            var result = new OperationResult<Paginator<OperationRecorderModel, TypeStatusOperationRecorder>>();

            var response = await _webServerService.DataBase.OperationRecorederController.GetByNamePageColumn(item,new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<OperationRecorderModel, TypeStatusOperationRecorder>()
                    {
                        Data = paginator.Data.ToOperationRecorder(),
                        DataType = (TypeStatusOperationRecorder)paginator.DataType,
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

        public async Task<OperationResult<IEnumerable<OperationRecorderModel>>> GetTaxServer(string pathFile, string passwordKey)
        {
            try
            {
                var result = Validation(pathFile, passwordKey);
                if (result.IsError)
                {
                    return result;
                }

                var operationRecorders = new List<OperationRecorderModel>();
                var resultOperation =_signingFileController.GetError(_signingFileController.GetDataToFile(pathFile, passwordKey));
                if (resultOperation.IsSuccess)
                {
                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _mainControllerHttp.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;

                    foreach (var item in infoUser.ElementAt(7).listValues)
                    {
                        OperationRecorderModel tempList = new OperationRecorderModel()
                        {
                            Status = item.STATUS,
                            Address = item.ADDRESS,
                            Name = item.NAME,
                        };
                        if (item.FNUM != null)
                        {
                            tempList.FiscalNumber = item.FNUM.ToString();
                        }
                        if (item.LNUM != null)
                        {
                            tempList.LocalNumber = item.LNUM.ToString();
                        }
                        if (item.STATUS == "Активний")
                        {
                            tempList.Status = item.STATUS;
                            tempList.TypeStatus = TypeStatusOperationRecorder.Open;
                        }
                        else if (item.STATUS == "Скасований")
                        {
                            tempList.Status = item.STATUS;
                            tempList.TypeStatus = TypeStatusOperationRecorder.Closed;
                        }

                        var time = item.D_REG;
                        if (time != null)
                        {
                            tempList.D_REG = DateTime.Parse(item.D_REG);
                        }
                        operationRecorders.Add(tempList);
                    }

                    result.Data = operationRecorders;
                    result.Status = ResultStatus.Success;
                    return result;
                }
                else if(resultOperation.IsError)
                {
                    return OperationResult<IEnumerable<OperationRecorderModel>>.Fail(resultOperation.ErrorMessage);
                }
                else
                {
                    return new OperationResult<IEnumerable<OperationRecorderModel>>()
                    {
                        ErrorMessage = "Невдалося викоанти операцію",
                        Status = ResultStatus.Error,
                    };
                } 
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<OperationRecorderModel>>()
                {
                    ErrorMessage = ex.Message,
                    Status = ResultStatus.Error,
                };
            }


        }
        private OperationResult<IEnumerable<OperationRecorderModel>> Validation(string pahtKey, string passwordKey)
        {
            if (string.IsNullOrEmpty(passwordKey))
            {
                return new OperationResult<IEnumerable<OperationRecorderModel>>()
                {
                    ErrorMessage = "Ведіть пароль ключа",
                    Status = ResultStatus.Error,
                    ErrorType = ErrorType.Validation,
                };
            }
            if (string.IsNullOrEmpty(pahtKey))
            {
                return new OperationResult<IEnumerable<OperationRecorderModel>>()
                {
                    ErrorMessage = "Виберіть ключ",
                    Status = ResultStatus.Error,
                    ErrorType = ErrorType.Validation,
                };
            }
            return new OperationResult<IEnumerable<OperationRecorderModel>>()
            {
                Status = ResultStatus.Success,
            };
        }

        public void SetOperationRecordeOnSession(OperationRecorderModel item)
        {
            _sessionService.UpdateOperationRecorder = item;
        }

        public OperationRecorderModel GetOperationrecorderInSession()
        {
            return _sessionService.UpdateOperationRecorder;
        }

        public async Task<OperationResult<bool>> UpdateParameter(string parameter, object value, OperationRecorderModel item)
        {
            var result = new OperationResult<bool>();
            var response = await _webServerService.DataBase.OperationRecorederController.UpdateParameter(parameter, value, item.ID.ToString());

            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;
            return result;
        }
    }
}
