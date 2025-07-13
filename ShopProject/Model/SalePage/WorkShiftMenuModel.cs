using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.Helpers;
using ShopProject.Helpers.FiscalOperationService;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.PrintingServise;
using ShopProjectDataBase.DataBase.Model;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.SalePage
{
    internal class WorkShiftMenuModel
    { 

        private PrintingDayReport _printingController;
        private FiscalOperationController _fiscalOperationController;

        public WorkShiftMenuModel()
        {  
            _printingController = new PrintingDayReport();

            _fiscalOperationController = new FiscalOperationController();
        }

        public bool OpenShift(OperationEntity operation)
        {

            if (_fiscalOperationController.OpenShift(operation)=="OK")
            {
                SaveDataBase(operation);
                return true;
            }
            return false;
        }   
        public bool CloseShift(OperationEntity operation)
        {
            if (_fiscalOperationController.CloseShift(operation) == "OK")
            {
                SaveDataBase(operation);
                return true;
            }
            return false;
        }
        public bool OfficialDepositMoney(OperationEntity operation)
        {
            if (_fiscalOperationController.DepositAndWithdrawalMoney(operation) == "OK")
            {
                SaveDataBase(operation);
                return true;
            }
            return false;
        }
        private bool SaveDataBase(OperationEntity operation)
        {
            bool result = false;
            operation.User = Session.User;
            Task t = Task.Run(async () =>
            {
                result = (await MainWebServerController.MainDataBaseConntroller.OperationController.AddOperation(Session.Token, operation));
            });
            return result;
        }

        public string? GetMac() => _fiscalOperationController.GetMac();

        public string? GetLocalNumber()
        {
            try
            {
                OperationEntity operation = new OperationEntity();

                Task t = Task.Run(async () =>
                {
                    operation = (await MainWebServerController.MainDataBaseConntroller.OperationController.GetLastOperation(Session.Token));
                });

                t.Wait();
                if (operation == null)
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
                return null;
            }
        }


        //public decimal GetNumberFiscalCheck()
        //{
        //    return _returnDataWithDataBase.GetTotalNumberFiscalChechAndReturnFiscalCheck(0);
        //}
        //public decimal GetNumberReturnFiscalCheck()
        //{
        //    return _returnDataWithDataBase.GetTotalNumberFiscalChechAndReturnFiscalCheck(1);
        //}
        //public decimal GetTotalFundsReceived()
        //{
        //    return _returnDataWithDataBase.GetTotalSumReceivedAndIssuance(2);
        //}
        //public decimal GetTotalFundsIssued() 
        //{
        //    return _returnDataWithDataBase.GetTotalSumReceivedAndIssuance(2.01m);
        //}
        //public decimal GetTotalBuyersAmountCash()
        //{
        //    return _returnDataWithDataBase.GetTotalBuyersAmountAndRestOperation("buyersAmount",0);
        //}
        //public decimal GetTotalRestCash() 
        //{
        //    return _returnDataWithDataBase.GetTotalBuyersAmountAndRestOperation("restPayment",0);
        //}
        //public decimal GetTotalBuyersAmountCard()
        //{
        //    return _returnDataWithDataBase.GetTotalBuyersAmountAndRestOperation("buyersAmount", 1);
        //}
        //public decimal GetTotalRestCard()
        //{
        //    return _returnDataWithDataBase.GetTotalBuyersAmountAndRestOperation("restPayment", 1);
        //}
        //public decimal GetTotatalChechReturnCash()
        //{
        //    return _returnDataWithDataBase.GetTotalRestReturnOperation(0);
        //}
        //public decimal GetTotatalChechReturnCard()
        //{
        //    return _returnDataWithDataBase.GetTotalRestReturnOperation(1);
        //}

        //public void Print(OperationEntity operation)
        //{
        //    _printingController.PrintCheck(operation);
        //}
    }
}
