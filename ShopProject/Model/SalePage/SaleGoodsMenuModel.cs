using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.Helpers;
using ShopProject.Helpers.FileServise.XmlServise;
using ShopProject.Helpers.NetworkServise.FiscalServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.PrintingServise;
using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.StoragePage;
using ShopProject.UIModel.UserPage;
using ShopProjectDataBase.Entities;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void AddKey(SignatureKey key) => _fiscalOperationController.AddKey(key);

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

        public bool SendCheck(ObservableCollection<ProductForSale> products, Operation operation)
        {
            var product = new List<Product>();
            foreach (var item in products) 
            {
                product.Add(item.Product);
            }

            var workingShift = Session.WorkingShiftStatus.WorkingShift;

            if (workingShift == null) 
            {
                return false;
            }
             

            var id =  _fiscalOperationController.SendFiscalCheck(workingShift,operation, product);
            if (id != string.Empty)
            {
                Task.Run(async () => {
                    await SaveDataBase(operation, product);
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


        private async Task SaveDataBase(Operation operation, List<Product> products)
        {
            try
            {
                operation.Shift = Session.WorkingShiftStatus.WorkingShift; 
                var result = (await MainWebServerController.MainDataBaseConntroller.OperationController.AddOperation(_token, operation));
                _operation = operation;
                _operation.ID = result;
                if (result >= 0)
                {
                    List<Order> orders = new List<Order>();
                    foreach (Product item in products)
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
            try
            {
                var mac = new MediaAccessControl()
                {
                    Operation = _operation,
                    OperationsRecorder = Session.WorkingShiftStatus.OperationRecorder,
                    Content = XmlServise.GenerationMACForXML(),
                    WorkingShifts = Session.WorkingShiftStatus.WorkingShift,
                };
                Session.WorkingShiftStatus.MediaAccessControl = mac;
                if (mac != null)
                {
                    return await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.AddMAC(_token, mac);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<MediaAccessControl> GetMAC(Guid operationRecorderId)
        {
            try
            {
                var mac = Session.WorkingShiftStatus.MediaAccessControl;
                if(mac != null && mac.Content != string.Empty)
                {
                    return mac;
                }
                return (await MainWebServerController.MainDataBaseConntroller.MediaAccessControlController.GetLastMAC(_token, operationRecorderId)).ToUIMediaAccessControl();
            } 
            catch
            {
                MessageBox.Show("Невдалося отримати MAC");
                return new MediaAccessControl();
            }
        }

        public async Task<string> GetLocalNumber()
        {
            try
            { 
                var item = await MainWebServerController.MainDataBaseConntroller.OperationController.GetLastNumberOperation(_token, Session.WorkingShiftStatus.WorkingShift.ID);

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
