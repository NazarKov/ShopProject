using ShopProject.Helpers;
using ShopProject.Helpers.DataGridViewHelperModel;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.SigningFileService;
using ShopProject.Helpers.SigningFileService.Model;
using ShopProjectDataBase.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace ShopProject.Model.AdminPage
{
    internal class OperationsRecorderModel
    {

        private SigningFileContoller _mainControllerTcp;
        private MainElectronicTaxAccountController _mainControllerHttp;

        private List<OperationsRecorderEntity> _softwareDeviceSettlementOperationsList;

        public OperationsRecorderModel()
        { 
            _softwareDeviceSettlementOperationsList = new List<OperationsRecorderEntity>();

            _mainControllerTcp = new SigningFileContoller();
            _mainControllerHttp = new MainElectronicTaxAccountController();
        }

        public List<OperationsRecorderEntity> GetAll()
        {
            try
            {
                Task t = Task.Run(async () =>
                {
                    _softwareDeviceSettlementOperationsList = (await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.GetOperationRecorders(Session.Token)).ToList();
                });
                t.Wait();
                return _softwareDeviceSettlementOperationsList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<OperationsRecorderEntity>();
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
                        TypeCommand = TypeCommand.Initialize,
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

                    foreach (var item in infoUser.ElementAt(7).listValues)
                    {
                        OperationsRecorderEntity tempList = new OperationsRecorderEntity()
                        {
                            Status = item.STATUS,
                            Address = item.ADDRESS,
                            FiscalNumber = item.FNUM.ToString(),
                            LocalNumber = item.LNUM.ToString(),
                            Name = item.NAME,
                        };

                        var time = item.D_REG;
                        if (time != null)
                        {
                            tempList.D_REG = DateTime.Parse(item.D_REG);
                        }
                        _softwareDeviceSettlementOperationsList.Add(tempList);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }


        }

        public List<OperationsRecorderEntity> SearchObject(string item)
        {
            try
            {
                var items = GetAll();
                if (items != null)
                {
                    if (item != " ")
                    {
                        return items.Where(i => i.Name.ToLower().Contains(item.ToLower())).ToList();
                    }
                }
                return new List<OperationsRecorderEntity>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<OperationsRecorderEntity>();
            }
        }

        public bool SaveDataBaseItem(List<SoftwareDeviceSettlementOperationsHelper> items)
        {
            try
            {
                List<OperationsRecorderEntity> result = new List<OperationsRecorderEntity>();
                for (int i = 0; i < items.Count; i++)
                {
                    if (items.ElementAt(i).isActive)
                    {
                        result.Add(items.ElementAt(i).deviceSettlementOperations);
                    }
                }
                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = (await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.AddOperationRecorders(Session.Token,result));
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
        public List<OperationsRecorderEntity> GetListObjecyOwner()
        {
            return _softwareDeviceSettlementOperationsList;
        }
        public bool deleteItemDataBase(OperationsRecorderEntity item)
        {
            try
            {
                //_operationRecorderTable.Delete(item);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public List<ObjectOwnerHelpers> GetAllObjectOwner()
        {
            try
            {
                List<ObjectOwnerHelpers> result = new List<ObjectOwnerHelpers>();

                List<ObjectOwnerEntity> list = new List<ObjectOwnerEntity>();
                
                Task t = Task.Run(async () =>
                {
                    list = (await MainWebServerController.MainDataBaseConntroller.ObjectOwnerController.GetObjectsOwners(Session.Token)).ToList();
                });
                t.Wait();

                foreach (var item in list)
                {
                    result.Add(new ObjectOwnerHelpers(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<ObjectOwnerHelpers>();
            }

        }
        public bool SaveBinding(OperationsRecorderEntity softwareDeviceSettlement, List<ObjectOwnerHelpers> objectOwnerHelpers)
        {
            try
            {
                if (objectOwnerHelpers.Where(item => item.isActive == true).ToList().Count() > 1)
                {
                    throw new Exception("Виберіть один обєкт");
                }
                bool result = false;
                Task t = Task.Run(async () =>
                {
                    result = (await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.AddBindingOperationRecorder(
                        Session.Token,
                        softwareDeviceSettlement.ID.ToString(), 
                        objectOwnerHelpers.Where(item => item.isActive).FirstOrDefault().item.ID.ToString()));
                });
                t.Wait();

                return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
