using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.Order;
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.UI.Product;
using ShopProject.Services.Integration.File.Xml;
using ShopProject.Services.Integration.Network.FiscalServerApi;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping;
using ShopProject.Services.Integration.Printing;
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Integration.PrintingService;
using ShopProject.Services.Modules.Model.WorkingShift.Interface;
using ShopProject.Services.Modules.Session.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Services.Modules.Model.WorkingShift
{
    internal class SaleMenuService : ISaleMenuService
    {
        private FiscalCheck _chek; 
        private bool _isDrawingChek;
        private MainFiscalServerController _fiscalOperationController;
        private Operation _operation;
        private readonly string _token;

        private IPrintingFiscalCheckService _printingFiscalCheckService;
        private ISessionService _sessionService;
        private IMainWebServerService _mainWebServerService;

        public bool IsDrawinfChek { get { return _isDrawingChek; } set { _isDrawingChek = value; } }

        public SaleMenuService(IPrintingFiscalCheckService printingFiscalCheckService,ISessionService sessionService,IMainWebServerService mainWebServerService)
        {
            _chek = new FiscalCheck(); 
            _printingFiscalCheckService = printingFiscalCheckService;
            _mainWebServerService = mainWebServerService;
            _isDrawingChek = true;

            _sessionService = sessionService;
            _token = _sessionService.User.Token;

            _fiscalOperationController = new MainFiscalServerController();
             
            _operation = new Operation(); 
        }

        public void AddKey(SignatureKey key) => _fiscalOperationController.AddKey(key);



        public async Task<bool> SendCheck(ObservableCollection<ProductForSaleModel> products, Operation operation)
        {

            var product = new List<Product>();
            foreach (var item in products)
            {
                product.Add(item.Product);
            }

            var workingShift = _sessionService.WorkingShiftStatus.WorkingShift;

            if (workingShift == null)
            {
                return false;
            }


            var id = _fiscalOperationController.SendReturnFiscalCheck(workingShift, operation, product);
            if (id != string.Empty)
            {
                operation.FiscalServerId = id;
                operation.MAC = CreateMac();
                await SaveDataBase(operation, product);
                PrintCheck(product, operation, id);

                return true;
            }
            return false;
        }

        public void PrintCheck(List<Product> products, Operation operation, string id)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (IsDrawinfChek)
                {
                    _chek.CreateFisckalCheck(products, operation, _sessionService.User, _sessionService.WorkingShiftStatus.OperationRecorder, _sessionService.WorkingShiftStatus.ObjectOwner);
                    _printingFiscalCheckService.PrintCheck(_chek.GetCheck());
                }
            });
        }


        private async Task SaveDataBase(Operation operation, List<Product> products)
        {
            try
            {
                operation.Shift = _sessionService.WorkingShiftStatus.WorkingShift;
                if (operation.Discount != null)
                {
                    operation.Discount.ID = (await _mainWebServerService.DataBase.DiscountController.AddDiscount(_token, operation.Discount));
                }
                var result = (await _mainWebServerService.DataBase.OperationController.AddOperation(_token, operation));
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
                    await _mainWebServerService.DataBase.OrderController.AddOrderRange(_token, orders);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private MediaAccessControl CreateMac()
        {
            return new MediaAccessControl()
            {
                Operation = _operation,
                OperationsRecorder = _sessionService.WorkingShiftStatus.OperationRecorder,
                Content = XmlServise.GenerationMACForXML(),
                WorkingShifts = _sessionService.WorkingShiftStatus.WorkingShift,
            };
        }

        public async Task<MediaAccessControl> GetMAC()
        {
            try
            {
                var mac =_sessionService.WorkingShiftStatus.MediaAccessControl;
                if (mac != null && mac.Content != string.Empty)
                {
                    return mac;
                }
                return (await _mainWebServerService.DataBase.MediaAccessControlController.GetLastMAC(_token, _sessionService.WorkingShiftStatus.OperationRecorder.ID)).ToMediaAccessControl();
            }
            catch
            { 
                return new MediaAccessControl();
            }
        }

        public async Task<string> GetLocalNumber()
        {
            try
            {
                var item = await _mainWebServerService.DataBase.OperationController.GetLastNumberOperation(_token, _sessionService.WorkingShiftStatus.WorkingShift.ID);

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
        public ShopProject.Model.Domain.User.User GetUserFromSession()
        {
            return _sessionService.User;
        }
    }
}
