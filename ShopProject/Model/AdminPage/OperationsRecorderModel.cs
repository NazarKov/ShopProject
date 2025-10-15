using ShopProject.Helpers;
using ShopProject.Helpers.DataGridViewHelperModel;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.UIModel.ObjectOwnerPage;
using ShopProject.UIModel.OperationRecorderPage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
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

        private List<OperationRecorder> _softwareDeviceSettlementOperationsList;
        private readonly string _token;
        public OperationsRecorderModel()
        { 
            _softwareDeviceSettlementOperationsList = new List<OperationRecorder>();
            _token = Session.User.Token;
            _signingFileController = new SigningFileContoller();
            _mainControllerHttp = new MainElectronicTaxAccountController();
            _signingFileController.Initialize(false);
        }


        public async Task<PaginatorData<OperationRecorder>> GetOperationsRecorderPageColumn(int page, int countColumn, TypeStatusOperationRecorder status)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.GetOperationsRecorderPageColumn(_token, page, countColumn, status);

                var paginator = new PaginatorData<OperationRecorder>()
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
                MessageBox.Show(ex.Message);
                return new PaginatorData<OperationRecorder>();
            }
        }

        public async Task<PaginatorData<OperationRecorder>> SearchByName(string item, int page, int countColumn, TypeStatusOperationRecorder status)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.GetOperationsRecorderByNamePageColumn(_token, item, page, countColumn, status);

                var paginator = new PaginatorData<OperationRecorder>()
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
                MessageBox.Show(ex.Message);
                return new PaginatorData<OperationRecorder>();
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
                        OperationRecorder tempList = new OperationRecorder()
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
                MessageBox.Show(ex.Message);
                return false;
            }


        } 

        public async Task<bool> SaveDataBaseItem(List<OperationRecorderDialogWindow> items)
        {
            try
            {
                List<OperationRecorder> result = new List<OperationRecorder>();
                for (int i = 0; i < items.Count; i++)
                {
                    if (items.ElementAt(i).isActive)
                    {
                        result.Add(items.ElementAt(i).deviceSettlementOperations);
                        
                    }
                }
                
                 return await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.AddOperationRecorders(_token,result.ToOperationRecorder());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);  
                return false;
            }

        }
        public List<OperationRecorder> GetListObjecyOwner()
        {
            return _softwareDeviceSettlementOperationsList;
        }
        public void ClearListObjectOwner()
        {
            _softwareDeviceSettlementOperationsList.Clear();
        }


        public async Task<bool> DeleteItem(OperationRecorder item)
        {
            try
            { 
                return await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.DeleteOperationsRecorder(_token,item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public async Task<List<ObjectOwnerDialogWindow>> GetAllObjectOwner()
        {
            try
            {
                var result = new List<ObjectOwnerDialogWindow>();  
                var items = await MainWebServerController.MainDataBaseConntroller.ObjectOwnerController.GetObjectsOwners(_token); 

                foreach (var item in items.ToObjectOwner())
                {
                    result.Add(new ObjectOwnerDialogWindow(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<ObjectOwnerDialogWindow>();
            }

        }
        public async Task<bool> SaveBinding(OperationRecorder softwareDeviceSettlement, List<ObjectOwnerDialogWindow> objectOwnerHelpers)
        {
            try
            {
                if (objectOwnerHelpers.Where(item => item.isActive == true).ToList().Count() > 1)
                {
                    throw new Exception("Виберіть один обєкт");
                }

                return await MainWebServerController.MainDataBaseConntroller.OperationRecorederController.AddBindingOperationRecorder(
                        _token,
                        softwareDeviceSettlement.ID.ToString(),
                        objectOwnerHelpers.Where(item => item.isActive).FirstOrDefault().item.ID.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
