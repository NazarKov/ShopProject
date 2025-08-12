using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.Helpers;
using ShopProject.Helpers.FiscalOperationService;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.PrintingServise; 
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
        private FiscalOperationController _fiscalOperationController;
          
        public bool IsDrawinfChek { get { return _isDrawingChek; } set { _isDrawingChek = value; } }
         
        public SaleGoodsMenuModel()
        { 
            _printingFiscalCheck = new PrintingFiscalCheck(); 

            _isDrawingChek = true; 

            _fiscalOperationController = new FiscalOperationController();
        }

        public ProductEntity? Search(string barCode)
        {
            try
            {
                List<ProductEntity> products = new List<ProductEntity>() { };

                if (products.Count <= 1)
                {

                    Task t = Task.Run(async () =>
                    {
                        products = (await MainWebServerController.MainDataBaseConntroller.ProductController.GetProducts(Session.Token)).ToList();
                    });
                    t.Wait();
                }
                return products.Where(item => item.Code == barCode).First();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public bool SendCheck(List<ProductEntity> products, OperationEntity operation)
        { 
            var id = _fiscalOperationController.SendFiscalCheck(operation, products);
            if (id != string.Empty)
            {
                SaveDataBase(operation, products);
                //_printingFiscalCheck.PrintCheck(products, id, order);

                return true;
            }
            return false;
        } 

        public void PrintCheck(List<ProductEntity> products, OperationEntity order, string id)
        {
            //_printingFiscalCheck.PrintCheck(products, id, order);
        }


        private void SaveDataBase(OperationEntity operation, List<ProductEntity> goods)
        {
            //operation.User = Session.User;
            bool result = false;
            Task t = Task.Run(async () =>
            {
                result = (await MainWebServerController.MainDataBaseConntroller.OperationController.AddOperation(Session.Token, operation));
                if (result)
                {
                    List<OrderEntity> orders = new List<OrderEntity>();
                    foreach (ProductEntity item in goods)
                    {
                        orders.Add(new OrderEntity()
                        {
                            Operation = operation,
                            //Goods = item,
                            Count = (int)item.Count,

                        });
                    }

                    await MainWebServerController.MainDataBaseConntroller.OrderController.AddOrderRange(Session.Token, orders);
                }
            });
        }

        public string GetMac() => _fiscalOperationController.GetMac();
         
        public string GetLocalNumber()
        {
            try
            {
                OperationEntity operation = new OperationEntity();

                Task t = Task.Run(async () =>
                {
                    operation = (await MainWebServerController.MainDataBaseConntroller.OperationController.GetLastOperation(Session.Token)); 
                });
                t.Wait();
                //if (operation.LocalNumbetShift == 0)
                //{
                //    return "1";
                //}

                return "1";

                //else
                //{
                //    return (Convert.ToInt32(operation.NumberPayment) + 1).ToString();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

    }
}
