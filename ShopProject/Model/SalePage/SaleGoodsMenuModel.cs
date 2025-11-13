using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.FiscalServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.PrintingServise;
using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
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
        private Operation _operation;
        private readonly string _token;
          
        public bool IsDrawinfChek { get { return _isDrawingChek; } set { _isDrawingChek = value; } }
         
        public SaleGoodsMenuModel()
        { 
            _printingFiscalCheck = new PrintingFiscalCheck(); 

            _isDrawingChek = true; 

            _fiscalOperationController = new MainFiscalServerController();

            _token = Session.User.Token;
            _operation = new Operation();
        }

        public async Task<Product>? Search(string barCode)
        {
            try
            {
                var item = await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductByBarCode(_token, barCode);
                if (item.ID == null) 
                {
                    return null; 
                }
                return item.ToProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public bool SendCheck(List<Product> products, Operation operation)
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


        private async Task SaveDataBase(Operation operation, List<Product> goods)
        {
            try
            {
                operation.Shift = Session.WorkingShift; 
                var result = (await MainWebServerController.MainDataBaseConntroller.OperationController.AddOperation(_token, operation));
                _operation = operation;
                _operation.ID = result;
                if (result != null)
                {
                    List<Order> orders = new List<Order>();
                    foreach (Product item in goods)
                    {
                        orders.Add(new Order()
                        {
                            Operation = operation,
                            Product = item,
                            Count = (int)item.Count,

                        });
                    } 
                    await MainWebServerController.MainDataBaseConntroller.OrderController.AddOrderRange(_token, orders);
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
                return await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.AddMAC(_token, new MediaAccessControl()
                {
                    Operation = _operation,
                    OperationsRecorder = Session.FocusDevices,
                    Content = mac,
                    WorkingShifts = Session.WorkingShift,
                });
            }

            return false;
        }

        public async Task<MediaAccessControl> GetMAC(Guid operationRecorderId)
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.GetLastMAC(_token, operationRecorderId)).ToUIMediaAccessControl();
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Невдалося отримати MAC");
                return new MediaAccessControl();
            }
        }

        public async Task<string> GetLocalNumber()
        {
            try
            { 
                var item = await MainWebServerController.MainDataBaseConntroller.OperationController.GetLastNumberOperation(_token, Session.WorkingShift.ID);

                if (item == string.Empty)
                {
                    return "1";
                } 
                else
                {
                    return (Convert.ToInt32(item) + 1).ToString();
                }
            }
            catch (Exception ex)
            { 
                return "1";
            }
        } 

    }
}
