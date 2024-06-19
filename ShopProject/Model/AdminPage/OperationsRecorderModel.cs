using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Entities;
using ShopProject.DataBase.Interfaces;
using ShopProject.Helpers;
using ShopProject.Helpers.DataGridViewHelperModel;
using ShopProject.Helpers.HttpService;
using ShopProject.Helpers.HttpService.Model;
using ShopProject.Helpers.SigningFileService;
using ShopProject.Helpers.SigningFileService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.AdminPage
{
    internal class OperationsRecorderModel
    {
        private IEntityUpdate<OperationsRecorderEntiti> _operationRecorderTableUpdate;
        private IEntityAccess<OperationsRecorderEntiti> _operationRecorderTable;
        private IEntityAccess<ObjectOwnerEntiti> _objectOwnerTable;

        private SigningFileContoller _mainControllerTcp;
        private HttpController _mainControllerHttp;

        private List<OperationsRecorderEntiti> _softwareDeviceSettlementOperationsList;

        public OperationsRecorderModel()
        {
            _operationRecorderTableUpdate = new OperationsRecorderTableAccess();
            _operationRecorderTable = new OperationsRecorderTableAccess();

            _objectOwnerTable = new ObjectOwnerTableAccess();

            _softwareDeviceSettlementOperationsList = new List<OperationsRecorderEntiti>();

            _mainControllerTcp = new SigningFileContoller();
            _mainControllerHttp = new HttpController();
        }
        public List<OperationsRecorderEntiti> GetAll()
        {
            try
            {
                return (List<OperationsRecorderEntiti>)_operationRecorderTable.GetAll();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<OperationsRecorderEntiti>();
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
                        OperationsRecorderEntiti tempList = new OperationsRecorderEntiti()
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

        public List<OperationsRecorderEntiti> SearchObject(string item)
        {
            try
            {
                var items = _operationRecorderTable.GetAll();
                if (items != null)
                {
                    if (item != " ")
                    {
                        return items.Where(i => i.Name.ToLower().Contains(item.ToLower())).ToList();
                    }
                }
                return new List<OperationsRecorderEntiti>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<OperationsRecorderEntiti>();
            }
        }

        public bool SaveDataBaseItem(List<SoftwareDeviceSettlementOperationsHelper> items)
        {
            try
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items.ElementAt(i).isActive)
                    {
                        _operationRecorderTable.Add(items.ElementAt(i).deviceSettlementOperations);
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
        public List<OperationsRecorderEntiti> GetListObjecyOwner()
        {
            return _softwareDeviceSettlementOperationsList;
        }
        public bool deleteItemDataBase(OperationsRecorderEntiti item)
        {
            try
            {
                _operationRecorderTable.Delete(item);
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
                foreach (var item in _objectOwnerTable.GetAll())
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
        public bool SaveBinding(OperationsRecorderEntiti softwareDeviceSettlement,List<ObjectOwnerHelpers> objectOwnerHelpers)
        {
            try
            {
                if (objectOwnerHelpers.Where(item => item.isActive == true).ToList().Count() > 1)
                {
                    throw new Exception("Виберіть один обєкт");
                }
                _operationRecorderTableUpdate.UpdateParameter(softwareDeviceSettlement.ID, nameof(softwareDeviceSettlement.ObjectOwner),
                    objectOwnerHelpers.Where(item=>item.isActive).FirstOrDefault().item.ID);

                return true;

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
