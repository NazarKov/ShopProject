using ShopProject.Helpers;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.ObjectOwner;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi;
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping; 
using ShopProject.Services.Modules.ModelService.OperationRecorder.Interface;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.ModelService.OperationRecorder
{
    internal class OperationRecorderServise : IOperationRecorderServise
    {
        private SigningFileContoller _signingFileController;
        private MainElectronicTaxAccountController _mainControllerHttp;

        private List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> _softwareDeviceSettlementOperationsList;
        private readonly string _token;
        private IMainWebServerService _webServerService;
        private ISessionService _sessionService;
        private ISettingService _settingService;

        public OperationRecorderServise(IMainWebServerService mainWebServerService,ISessionService sessionService,ISettingService settingService)
        {
            _webServerService = mainWebServerService;
            _sessionService = sessionService;
            _settingService = settingService;
            _token = _sessionService.User.Token;

            _softwareDeviceSettlementOperationsList = new List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>();
            _signingFileController = new SigningFileContoller();
            _mainControllerHttp = new MainElectronicTaxAccountController();
            _signingFileController.Initialize(false);
        }


        public async Task<Paginator<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>> GetOperationsRecorderPageColumn(int page, int countColumn, TypeStatusOperationRecorder status)
        {
            try
            {
                var result = await _webServerService.DataBase.OperationRecorederController.GetOperationsRecorderPageColumn(_token, page, countColumn, status);

                var paginator = new Paginator<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>()
                {
                    Data = result.Data.ToOperationRecorder(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages
                };
                return paginator;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new Paginator<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>();
            }
        }

        public async Task<Paginator<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>> SearchByName(string item, int page, int countColumn, TypeStatusOperationRecorder status)
        {
            try
            {
                var result = await _webServerService.DataBase.OperationRecorederController.GetOperationsRecorderByNamePageColumn(_token, item, page, countColumn, status);

                var paginator = new Paginator<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>()
                {
                    Data = result.Data.ToOperationRecorder(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages
                };
                return paginator;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new Paginator<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>();
            }
        }

        public async Task<bool> GetServerSoftwareDeviceSettlementOperations(string pathFile, string passwordKey)
        {
            try
            {
                if (passwordKey == null)
                {
                    throw new Exception("Ведіть пароль");
                }


                if (_signingFileController.GetDataToFile(pathFile, passwordKey))
                {
                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _mainControllerHttp.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;

                    foreach (var item in infoUser.ElementAt(7).listValues)
                    {
                        ShopProject.Model.Domain.OperationRecorder.OperationRecorder tempList = new ShopProject.Model.Domain.OperationRecorder.OperationRecorder()
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
                        _softwareDeviceSettlementOperationsList.Add(tempList);
                    }

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }


        }

        public async Task<bool> SaveDataBaseItem(List<OperationRecorderDialogWindowModel> items)
        {
            try
            {
                List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> result = new List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>();
                for (int i = 0; i < items.Count; i++)
                {
                    if (items.ElementAt(i).isActive)
                    {
                        //result.Add(items.ElementAt(i).deviceSettlementOperations);

                    }
                }

                return await _webServerService.DataBase.OperationRecorederController.AddOperationRecorders(_token, result.ToOperationRecorder());

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }

        }
        public List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> GetListObjecyOwner()
        {
            return _softwareDeviceSettlementOperationsList;
        }
        public void ClearListObjectOwner()
        {
            _softwareDeviceSettlementOperationsList.Clear();
        }


        public async Task<bool> DeleteItem(ShopProject.Model.Domain.OperationRecorder.OperationRecorder item)
        {
            try
            {
                return await _webServerService.DataBase.OperationRecorederController.DeleteOperationsRecorder(_token, item);
            }
            catch (Exception ex)
            {
                /*MessageBox.Show(ex.Message)*/;
                return false;
            }
        }
        public async Task<List<ObjectOwnerDialogWindowModel>> GetAllObjectOwner()
        {
            try
            {
                var result = new List<ObjectOwnerDialogWindowModel>();
                var items = await _webServerService.DataBase.ObjectOwnerController.GetObjectsOwners(_token);

                foreach (var item in items.ToObjectOwner())
                {
                    result.Add(new ObjectOwnerDialogWindowModel(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new List<ObjectOwnerDialogWindowModel>();
            }

        }
        public async Task<bool> SaveBinding(ShopProject.Model.Domain.OperationRecorder.OperationRecorder softwareDeviceSettlement, List<ObjectOwnerDialogWindowModel> objectOwnerHelpers)
        {
            try
            {
                if (objectOwnerHelpers.Where(item => item.isActive == true).ToList().Count() > 1)
                {
                    throw new Exception("Виберіть один обєкт");
                }

                return await _webServerService.DataBase.OperationRecorederController.AddBindingOperationRecorder(
                        _token,
                        softwareDeviceSettlement.ID.ToString(),
                        objectOwnerHelpers.Where(item => item.isActive).FirstOrDefault().ObjectOwner.ID.ToString());

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>> GetAllOperationsRecorderOperationsUser()
        {
            try
            {
                var item = (await _webServerService.DataBase.OperationRecorderAndUserController.GetOperationRecordersAndUser(_token));


                var result = item.OpertionsRecorders.ToOperationRecorder();
                return result.ToList();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>();
            }
        }

        //public async Task<List<Model.Domain.OperationRecorder.OperationRecorder>> Search(string item)
        //{
        //    try
        //    {
        //        return (await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.GetOperationRecordersByNumberAndUser(_token, item, Session.User.ID)).ToOperationRecorder().ToList();

        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //        return new List<Model.Domain.OperationRecorder.OperationRecorder>();
        //    }
        //}

        public void SetOperationRecorderOnWorkingShiftStatusInSession(ShopProject.Model.Domain.OperationRecorder.OperationRecorder operationRecorder)
        {
            if (_sessionService.WorkingShiftStatus == null)
            {
                _sessionService.WorkingShiftStatus = new ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus();
            }

            _sessionService.WorkingShiftStatus.OperationRecorder = operationRecorder;
        }
        public ShopProject.Model.Domain.OperationRecorder.OperationRecorder GerOperationRecorderOnWorkingShiftStatusFromSession()
        {
            var result = _sessionService.WorkingShiftStatus;

            if(result !=null&& result.OperationRecorder!= null)
            {
                return result.OperationRecorder;
            }

            throw new Exception("Невдалося завантажити ресурси");
        }
        public OperationRecorderSetting GetSetting()
        {
            return _settingService.GetSetting<OperationRecorderSetting>();
        }

    }
}
