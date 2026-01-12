using ShopProject.Helpers; 
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.UIModel.ObjectOwnerPage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZXing;

namespace ShopProject.Model.AdminPage
{
    internal class ObjectOwnerShipModel
    {
        private SigningFileContoller _signingFileController;
        private MainElectronicTaxAccountController _accountController;
        private List<ObjectOwner> _objectOwnerList;
        private readonly string _token;

        public ObjectOwnerShipModel()
        {
            _objectOwnerList = new List<ObjectOwner>();

            _signingFileController = new SigningFileContoller();
            _accountController = new MainElectronicTaxAccountController();
            _signingFileController.Initialize(false);
            _token = Session.User.Token;
        }

        public async Task<PaginatorData<ObjectOwner>> GetObjectOwnerPageColumn(int page, int countColumn, TypeStatusObjectOwner status)
        {
            try
            {
                var items = await MainWebServerController.MainDataBaseConntroller.ObjectOwnerController.GetObjectsOwnersPageColumn(_token, page, countColumn, status);

                var paginator = new PaginatorData<ObjectOwner>()
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
                MessageBox.Show(ex.Message);
                return new PaginatorData<ObjectOwner>();
            }
        }

        public async Task<PaginatorData<ObjectOwner>> SearchByName(string item, int page, int countColumn, TypeStatusObjectOwner status)
        {
            try
            {
                var items = await MainWebServerController.MainDataBaseConntroller.ObjectOwnerController.GetObjectsOwnersByNamePageColumn(_token, item, page, countColumn, status);
                var paginator = new PaginatorData<ObjectOwner>()
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
                MessageBox.Show(ex.Message);
                return new PaginatorData<ObjectOwner>();
            }
        } 

        public async Task<bool> GetServerObjectOwner(string pathFile, string passwordKey)
        {
            try
            {

                _objectOwnerList = new List<ObjectOwner>();
                if (_signingFileController.GetDataToFile(pathFile, passwordKey))
                {
                   
                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _accountController.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;

                    foreach (var item in infoUser.ElementAt(8).listValues)
                    {

                        ObjectOwner objectOwner = new ObjectOwner()
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
                MessageBox.Show(ex.Message);
                return false;
            }


        }

        public bool SaveDataBaseItem(List<ObjectOwnerDialogWindow> objectOwnerHelpers)
        {
            try
            {

                List<ObjectOwner> result = new List<ObjectOwner>();
                for (int i = 0; i < objectOwnerHelpers.Count; i++)
                {
                    if (objectOwnerHelpers.ElementAt(i).isActive)
                    {
                        result.Add(objectOwnerHelpers.ElementAt(i).item);
                    }

                }
                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = await MainWebServerController.MainDataBaseConntroller.ObjectOwnerController.AddObjectsOwners(_token, result.ToObjectOwner());
                });

                t.Wait();
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public List<ObjectOwner> GetListObjectOwner()
        {
            return _objectOwnerList;
        }
        public async Task<bool> DeleteItem(ObjectOwner item)
        {
            try
            { 
                return await MainWebServerController.MainDataBaseConntroller.ObjectOwnerController.DeleteObjectsOwner(_token, item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
