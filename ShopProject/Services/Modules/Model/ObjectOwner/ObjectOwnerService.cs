using ShopProject.Helpers;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.ObjectOwner;
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi;
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping; 
using ShopProject.Services.Modules.ModelService.ObjectOwner.Interface;
using ShopProject.Services.Modules.Session.Interface;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Aztec.Internal;

namespace ShopProject.Services.Modules.ModelService.ObjectOwner
{
    internal class ObjectOwnerService : IObjectOwnerService
    {
        private SigningFileContoller _signingFileController;
        private MainElectronicTaxAccountController _accountController;
        private List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner> _objectOwnerList;
        private readonly string _token;
        private IMainWebServerService _webServerService;
        private ISessionService _sessionService;

        public ObjectOwnerService(IMainWebServerService mainWebServerService,ISessionService sessionService)
        {
            _webServerService = mainWebServerService;
            _sessionService = sessionService;

            _objectOwnerList = new List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>();

            _signingFileController = new SigningFileContoller();
            _accountController = new MainElectronicTaxAccountController();
            _signingFileController.Initialize(false);
            _token = _sessionService.User.Token;
        } 

        public async Task<Paginator<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>> GetObjectOwnerPageColumn(int page, int countColumn, TypeStatusObjectOwner status)
        {
            try
            {
                var items = await _webServerService.DataBase.ObjectOwnerController.GetObjectsOwnersPageColumn(_token, page, countColumn, status);

                var paginator = new Paginator<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>()
                {
                    Data = items.Data.ToObjectOwner(),
                    DataType = items.DataType,
                    Page = items.Page,
                    Pages = items.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new Paginator<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>();
            }
        }

        public async Task<Paginator<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>> SearchByName(string item, int page, int countColumn, TypeStatusObjectOwner status)
        {
            try
            {
                var items = await _webServerService.DataBase.ObjectOwnerController.GetObjectsOwnersByNamePageColumn(_token, item, page, countColumn, status);
                var paginator = new Paginator<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>()
                {
                    Data = items.Data.ToObjectOwner(),
                    DataType = items.DataType,
                    Page = items.Page,
                    Pages = items.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new Paginator<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>();
            }
        }

        public async Task<bool> GetServerObjectOwner(string pathFile, string passwordKey)
        {
            try
            {

                _objectOwnerList = new List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>();
                if (_signingFileController.GetDataToFile(pathFile, passwordKey))
                {

                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _accountController.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;

                    foreach (var item in infoUser.ElementAt(8).listValues)
                    {

                        ShopProject.Model.Domain.ObjectOwner.ObjectOwner objectOwner = new ShopProject.Model.Domain.ObjectOwner.ObjectOwner()
                        {
                            NameOwner = nameUser,
                            NameObject = item.NAME,
                            Address = item.ADDRESS,
                            C_DISTR = item.C_DISTR,
                            TypeOfRights = item.TYPE_OF_RIGHTS,
                            KATOTTG = item.KATOTTG,

                        };
                        if (objectOwner.C_TERRIT != null)
                        {
                            objectOwner.C_TERRIT = item.C_TERRIT.ToString();
                        }
                        if (objectOwner.REG_NUM_OBJ != null)
                        {
                            objectOwner.REG_NUM_OBJ = item.REG_NUM_OBJ.ToString();
                        }
                        if (objectOwner.TypeObjectName != null)
                        {
                            objectOwner.TypeObjectName = item.TYPE_OF_RIGHTS.ToString();
                        }


                        if (item.STAN_OBJECT == "Об'єкт відчужено / повернено власнику")
                        {
                            objectOwner.TypeStatus = TypeStatusObjectOwner.Closed;
                        }
                        else if (item.STAN_OBJECT == "орендується")
                        {
                            objectOwner.TypeStatus = TypeStatusObjectOwner.Open;
                        }
                        objectOwner.Status = item.STAN_OBJECT;

                        var time = item.D_ACC_START;
                        if (time != null)
                        {
                            objectOwner.D_ACC_START = DateTime.Parse(item.D_ACC_START);
                        }
                        time = item.D_LAST_CH;
                        if (time != null)
                        {
                            objectOwner.D_LAST_CH = DateTime.Parse(item.D_LAST_CH);
                        }
                        time = item.D_ACC_END;
                        if (time != null)
                        {
                            objectOwner.D_ACC_END = DateTime.Parse(item.D_ACC_END);
                        }



                        _objectOwnerList.Add(objectOwner);
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

        public bool SaveDataBaseItem(List<ObjectOwnerDialogWindowModel> objectOwnerHelpers)
        {
            try
            {

                List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner> result = new List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>();
                for (int i = 0; i < objectOwnerHelpers.Count; i++)
                {
                    if (objectOwnerHelpers.ElementAt(i).isActive)
                    {
                        result.Add(objectOwnerHelpers.ElementAt(i).ObjectOwner);
                    }

                }
                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = await _webServerService.DataBase.ObjectOwnerController.AddObjectsOwners(_token, result.ToObjectOwner());
                });

                t.Wait();
                return response;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }

        }

        public List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner> GetListObjectOwner()
        {
            return _objectOwnerList;
        }
        public async Task<bool> DeleteItem(ShopProject.Model.Domain.ObjectOwner.ObjectOwner item)
        {
            try
            {
                return await _webServerService.DataBase.ObjectOwnerController.DeleteObjectsOwner(_token, item);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>> GetObjectOwners()
        {
            try
            {
                var item = (await _webServerService.DataBase.ObjectOwnerController.GetObjectsOwners(_token));

                var result = item.ToObjectOwner();
                return result.ToList();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>();
            }
        }
        public void SetObjectOwnerOnWorkingShiftStatusInSession(ShopProject.Model.Domain.ObjectOwner.ObjectOwner objectOwner)
        {
            if (_sessionService.WorkingShiftStatus == null)
            {
                _sessionService.WorkingShiftStatus = new ShopProject.Model.Domain.WorkingShift.WorkingShiftStatus();
            }
            _sessionService.WorkingShiftStatus.ObjectOwner = objectOwner;
        }
        public ShopProject.Model.Domain.ObjectOwner.ObjectOwner GetObjectOwnerOnWorkingShiftStatusFromSession()
        {
            var result = _sessionService.WorkingShiftStatus;
            if(result!= null && result.ObjectOwner != null)
            {
                return result.ObjectOwner;
            }

            throw new Exception("Невдалося завантажити ресурси"); 
        }
    }
}
