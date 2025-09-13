using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.FiscalServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.PrintingServise;
using ShopProject.UIModel;
using ShopProject.UIModel.SalePage;
using ShopProjectSQLDataBase.Entities;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.SalePage
{
    internal class SaleGoodsMenuModel
    {
         
        private PrintingFiscalCheck _printingFiscalCheck;
        private bool _isDrawingChek; 
        private MainFiscalServerController _fiscalOperationController;
          
        public bool IsDrawinfChek { get { return _isDrawingChek; } set { _isDrawingChek = value; } }
         
        public SaleGoodsMenuModel()
        { 
            _printingFiscalCheck = new PrintingFiscalCheck(); 

            _isDrawingChek = true; 

            _fiscalOperationController = new MainFiscalServerController();
        }

        public async Task<ProductEntity>? Search(string barCode)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductByBarCode(Session.Token,barCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public bool SendCheck(List<ProductEntity> products, UIOperationModel operation)
        {
            var id =  _fiscalOperationController.SendFiscalCheck(Session.WorkingShift,operation, products);
            if (id != string.Empty)
            {
                Task.Run(async () => {
                    await SaveDataBase(operation, products);
                    await CreateMac();
                });
                //_printingFiscalCheck.PrintCheck(products, id, order);

                return true;
            }
            return false;
        } 

        public void PrintCheck(List<ProductEntity> products, OperationEntity order, string id)
        {
            //_printingFiscalCheck.PrintCheck(products, id, order);
        }


        private async Task SaveDataBase(UIOperationModel operation, List<ProductEntity> goods)
        {
            try
            {
                operation.Shift = Session.WorkingShift;
                bool result = false;
                result = (await MainWebServerController.MainDataBaseConntroller.OperationController.AddOperation(Session.Token, operation));
                if (result)
                {
                    List<UIOrderModel> orders = new List<UIOrderModel>();
                    foreach (ProductEntity item in goods)
                    {
                        orders.Add(new UIOrderModel()
                        {
                            Operation = operation,
                            Product = item,
                            Count = (int)item.Count,

                        });
                    } 
                    await MainWebServerController.MainDataBaseConntroller.OrderController.AddOrderRange(Session.Token, orders);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async Task<bool?> CreateMac()
        {

            var mac = WriteReadXmlFile.GenerationMACForXML();

            if (mac != null)
            {
                return await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.AddMAC(Session.Token, new UIMediaAccessControlModel()
                {
                    OperationsRecorder = Session.FocusDevices,
                    Content = mac,
                    WorkingShifts = Session.WorkingShift,
                });
            }

            return false;
        }

        public async Task<UIMediaAccessControlModel> GetMAC(Guid operationRecorderId)
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.GetLastMAC(Session.Token, operationRecorderId)).ToUIMediaAccessControl();
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Невдалося отримати MAC");
                return new UIMediaAccessControlModel();
            }
        }

        public async Task<string> GetLocalNumber()
        {
            try
            {
                OperationEntity operation = new OperationEntity();
                 
                operation = await MainWebServerController.MainDataBaseConntroller.OperationController.GetLastOperation(Session.Token,Session.WorkingShift.ID);

                if (operation.NumberPayment == string.Empty)
                {
                    return "1";
                } 
                else
                {
                    return (Convert.ToInt32(operation.NumberPayment) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "1";
            }
        }

    }
}
