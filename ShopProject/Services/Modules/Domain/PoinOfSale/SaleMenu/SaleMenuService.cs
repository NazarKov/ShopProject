using ShopProject.Model.Domain.Discount; 
using ShopProject.Model.Domain.Operation; 
using ShopProject.Model.Enum;  
using ShopProject.Services.Integration.Network.FiscalServerApi; 
using ShopProject.Services.Integration.Network.WebServerApi.Interface; 
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Integration.PrintingService;
using ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface; 
using ShopProject.Services.Modules.Model.WorkingShift.Interface;
using ShopProject.Services.Modules.Session.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ProductModel = ShopProject.Model.Domain.Product.Product;

namespace ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu
{
    internal class SaleMenuService : ISaleMenuService
    {
        private FiscalCheck _chek;   
        private IPrintingFiscalCheckService _printingFiscalCheckService;
        private ISessionService _sessionService;  
        private IWorkingShfitOperationService _workingShfitOperationService; 

        public SaleMenuService(IPrintingFiscalCheckService printingFiscalCheckService,ISessionService sessionService,IWorkingShfitOperationService workingShfitOperationService)
        {
            _workingShfitOperationService = workingShfitOperationService;

            _chek = new FiscalCheck(); 
            _printingFiscalCheckService = printingFiscalCheckService;  
            _sessionService = sessionService;  
        }
         
        

        private void PrintCheck(List<ProductModel> products, Operation operation, string id,bool _isdrawingchek = true)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_isdrawingchek)
                {
                    _chek.CreateFisckalCheck(products, operation, _sessionService.User, _sessionService.WorkingShiftStatus.OperationRecorder, _sessionService.WorkingShiftStatus.TaxObject);
                    _printingFiscalCheckService.PrintCheck(_chek.GetCheck());
                }
            });
        }

        public async Task SendCheck(OperationSaleInfo operationSaleInfo)
        {
            var workingShift = _sessionService.WorkingShiftStatus.WorkingShift;

        

            var result = await _workingShfitOperationService.GetWorkingShiftResourse(workingShift.FiscalNumberRRO);
            if (result.IsSuccess)
            {

                var rest = (operationSaleInfo.SumaUser - operationSaleInfo.SumaOrder);
                var discount = new Discount();

                if (operationSaleInfo.DiscountPrecent != 0)
                {
                    discount.TotalDiscount = operationSaleInfo.TotalSum * (operationSaleInfo.DiscountPrecent / 100);
                    discount.TypeDiscount = 1;
                    discount.CreateAt = DateTime.Now;
                    discount.InterimAmount = operationSaleInfo.TotalSum;
                    discount.Rebate = operationSaleInfo.DiscountPrecent;
                }
                else if (operationSaleInfo.Discount != 0)
                {
                    discount.TotalDiscount = operationSaleInfo.Discount;
                    discount.TypeDiscount = 0;
                    discount.CreateAt = DateTime.Now;
                    discount.InterimAmount = operationSaleInfo.TotalSum;
                    discount.Rebate = operationSaleInfo.Discount;
                }
                else
                {
                    discount = null;
                }


                Operation operation = new Operation()
                {
                    TypeOperation = TypeOperation.FiscalCheck,
                    MAC = result.Data.MediaAccessControl,
                    CreatedAt = DateTime.Now,
                    NumberPayment = result.Data.OperationNumber,
                    GoodsTax = "0",
                    RestPayment = rest.Value,
                    TotalPayment = operationSaleInfo.TotalSum,
                    BuyersAmount = operationSaleInfo.SumaUser.Value,
                    TypePayment = operationSaleInfo.TypePayment,
                    Discount = discount,
                };

                await _workingShfitOperationService.SendCheck(operationSaleInfo.Products, operation);


                PrintCheck(operationSaleInfo.Products.ToList(), operation, "", operationSaleInfo.DrawingCheck);
            } 
             
        }

       

        
        public ShopProject.Model.Domain.User.User GetUserFromSession()
        {
            return _sessionService.User;
        }
    }
}
