using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Entities;
using ShopProject.DataBase.Interfaces;
using ShopProject.Helpers.DataGridViewHelperModel;
using ShopProject.Helpers.HttpService;
using ShopProject.Helpers.HttpService.Model;
using ShopProject.Helpers.SigningFileService;
using ShopProject.Helpers.SigningFileService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.AdminPage
{
    internal class ObjectOwnerShipModel
    {
        private IEntityAccess<ObjectOwnerEntiti> _objectTable;

        private SigningFileContoller _mainControllerTcp;
        private HttpController _mainControllerHttp;

        private List<ObjectOwnerEntiti> _objectOwnerList;

        public ObjectOwnerShipModel()
        {
            _objectTable = new ObjectOwnerTableAccess();

            _objectOwnerList = new List<ObjectOwnerEntiti>();

            _mainControllerTcp = new SigningFileContoller();
            _mainControllerHttp = new HttpController();
        }


        public List<ObjectOwnerEntiti> GetAll() => (List<ObjectOwnerEntiti>)_objectTable.GetAll();

        public List<ObjectOwnerEntiti> SearchObject(string item)
        {
            try
            {
                var items = _objectTable.GetAll();
                if (items != null)
                {
                    if(item!= " ")
                    {
                        return items.Where(i => i.NameObject.ToLower().Contains(item.ToLower())).ToList();
                    }
                }
                return new List<ObjectOwnerEntiti>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<ObjectOwnerEntiti>();
            }
        }

        public async Task<bool> GetServerObjectOwner(string pathFile, string passwordKey)
        {
            try
            {
                if (passwordKey == null)
                {
                    throw new Exception("Ведіть пароль");
                }
                if (!_mainControllerTcp.IsConnectingServise())
                {
                    if (!_mainControllerTcp.IsStartServise())
                    {
                        _mainControllerTcp.StartServise();
                    }
                    _mainControllerTcp.ConnectService();
                }

                var result = _mainControllerTcp.SendingCommand(new UserCommand()
                {
                    TypeCommand = TypeCommand.IsInitialize,
                    Time = DateTime.Now,
                });

                if (result.Status == "404")
                {
                    _mainControllerTcp.SendingCommand(new UserCommand()
                    {
                        TypeCommand =   TypeCommand.Initialize,
                        Time = DateTime.Now,
                    });
                }
                result = _mainControllerTcp.SendingCommand(new UserCommand()
                {
                    TypeCommand = TypeCommand.GetDataKey,
                    PathKey = pathFile,
                    PasswordKey = passwordKey,
                    Time = DateTime.Now,
                });

                if (result.Status == "100")
                {
                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _mainControllerHttp.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;

                    foreach (var item in infoUser.ElementAt(8).listValues)
                    {

                        ObjectOwnerEntiti objectOwner = new ObjectOwnerEntiti()
                        {
                            NameObject = item.NAME,
                            Status = item.STAN_OBJECT,
                            Address = item.ADDRESS,
                            C_DISTR = item.C_DISTR,
                            TypeOfRights = item.TYPE_OF_RIGHTS,
                            C_TERRIT = item.C_TERRIT.ToString(),
                            KATOTTG = item.KATOTTG,
                            REG_NUM_OBJ = item.REG_NUM_OBJ,
                            CodeObject = item.TO_CODE.ToString(),
                            TypeObjectName = item.TYPE_OF_RIGHTS.ToString(),

                        };

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

                    _mainControllerTcp.SendingCommand(new UserCommand()
                    {
                        TypeCommand = TypeCommand.DisconnectUser,
                        Time = DateTime.Now,
                    });
                    return true;
                }
                return false;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return false;
            }


        }

        public bool SaveDataBaseItem(List<ObjectOwnerHelpers> objectOwnerHelpers)
        {
            try
            {
                for (int i = 0; i < objectOwnerHelpers.Count; i++)
                {
                    if (objectOwnerHelpers.ElementAt(i).isActive)
                    {
                        _objectTable.Add(objectOwnerHelpers.ElementAt(i).item);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        public List<ObjectOwnerEntiti> GetListObjecyOwner()
        {
            return _objectOwnerList;
        }
        public bool deleteItemDataBase(ObjectOwnerEntiti item)
        {
            try
            {
                _objectTable.Delete(item);
                return true;
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
