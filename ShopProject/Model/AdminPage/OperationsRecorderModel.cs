using ShopProject.Helpers;
using ShopProject.Helpers.DataGridViewHelperModel;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.Template.Paginator;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using SigningFileLib;
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

        private SigningFileContoller _signingFileController;
        private MainElectronicTaxAccountController _mainControllerHttp;

        private List<OperationsRecorderEntity> _softwareDeviceSettlementOperationsList;

        public OperationsRecorderModel()
        { 
            _softwareDeviceSettlementOperationsList = new List<OperationsRecorderEntity>();

            _signingFileController = new SigningFileContoller();
            _mainControllerHttp = new MainElectronicTaxAccountController();
            _signingFileController.Initialize(false);
        }


        public async Task<PaginatorData<OperationsRecorderEntity>> GetOperationsRecorderPageColumn(int page, int countColumn, TypeStatusOperationRecorder status)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.GetOperationsRecorderPageColumn(Session.Token, page, countColumn, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<OperationsRecorderEntity>();
            }
        }

        public async Task<PaginatorData<OperationsRecorderEntity>> SearchByName(string item, int page, int countColumn, TypeStatusOperationRecorder status)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.GetOperationsRecorderByNamePageColumn(Session.Token, item, page, countColumn, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<OperationsRecorderEntity>();
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


                if (_signingFileController.GetDataToFile(pathFile,passwordKey))
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
                MessageBox.Show(ex.Message);
                return false;
            }


        } 

        public async Task<bool> SaveDataBaseItem(List<SoftwareDeviceSettlementOperationsHelper> items)
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
                
                 return await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.AddOperationRecorders(Session.Token,result);
                
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
        public void ClearListObjectOwner()
        {
            _softwareDeviceSettlementOperationsList.Clear();
        }


        public async Task<bool> DeleteItem(OperationsRecorderEntity item)
        {
            try
            {
                
                return await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.DeleteOperationsRecorder(Session.Token,item);
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
