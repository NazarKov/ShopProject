using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Helpers.PrintingServise;
using ShopProject.Helpers.SigningFileService;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ShopProject.Model.SalePage
{
    internal class WorkShiftMenuModel
    {
        private IEntityAccess<OperationEntiti> _operationRepository;
        private IEntityAccess<OrderEntiti> _goodsOperationRepository;

        private ReturnDataWithDataBase _returnDataWithDataBase;

        string pathxml = "C:\\ProgramData\\ShopProject\\Temp\\Chek.xml";

        private SigningFileContoller _signFileContoller;
        private FiscalServerController _serverController;
        private PrintingDayReport _printingController;

        public WorkShiftMenuModel()
        {
            _operationRepository = new OperationTableAccess();
            _goodsOperationRepository = new OrderTableAccess();

            _returnDataWithDataBase = new ReturnDataWithDataBase();
            _signFileContoller = new SigningFileContoller();
            _serverController = new FiscalServerController();
            _printingController = new PrintingDayReport();


        }

        public bool OpenShift(OperationEntiti operation, bool @checked)
        {
           return OpenShiftRecursive(operation, @checked, 0,5);
        }
        private bool OpenShiftRecursive(OperationEntiti operation, bool @checked,int depth, int maxDepth)
        {
            try
            {
                if (depth >= maxDepth)
                {
                    throw new Exception("Неможливо виконати операцію");
                }
                InspectionOpeningShift(operation, @checked);
                return true;
            }
            catch (ExceptionOK)
            {
                SaveDataBase(operation);
                MessageBox.Show("Змінна відкрита", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (ExceptionBadHashPrev exbadhash)
            {
                operation.MAC = exbadhash.Error.Split(" ")[3];
                return OpenShiftRecursive(operation, false, depth + 1, maxDepth);
            }
            catch (ExceptionCheck exCheck)
            {
                MessageBox.Show(exCheck.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            catch (ExceptionSave exSave)
            {
                MessageBox.Show(exSave.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }
        private void InspectionOpeningShift(OperationEntiti operation, bool @checked)
        {
            if (@checked && operation.MAC != null)
            {
                CheckedCloseShift();
            }
            OpenShift(operation);
        }
        private void OpenShift(OperationEntiti operation)
        {
            WriteReadXmlFile.WriteXmlFile(operation, new List<OrderEntiti>(), new List<ProductEntiti>(), pathxml);
            if (_signFileContoller.SignFiles(Session.User.KeyPath, Session.User.KeyPassword))
            {
                _serverController.SendServiceCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss"))
                    , Convert.ToInt32(operation.NumberPayment), 
                    operation.FiscalNumberRRO,(bool)AppSettingsManager.GetParameterFiles("TestMode"));
            }
            else
            {
               throw new Exception("невдалося підписати файл");
            }
        }

        public bool CloseShift(OperationEntiti operation, bool @checked)
        {
           return CloseShiftRecursive(operation, @checked, 0, 5);

        }
        private bool CloseShiftRecursive(OperationEntiti operation, bool @checked, int depth, int maxDepth)
        {
            try
            {
                if (depth >= maxDepth)
                {
                    throw new Exception("Неможливо виконати операцію");
                }

                InspectionEndShift(operation, @checked);
                return true;
            }
            catch (ExceptionOK)
            {
                SaveDataBase(operation);
                MessageBox.Show("Змінна закрита", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (ExceptionBadHashPrev exbadHash)
            {
                operation.MAC = exbadHash.Error.Split(" ")[3];
                return CloseShiftRecursive(operation,@checked, depth+1, maxDepth);
            }
            catch (ExceptionSave exSave)
            {
                operation.MAC = string.Empty;
                return CloseShiftRecursive(operation, @checked, depth + 1, maxDepth);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

        }
        private void InspectionEndShift(OperationEntiti operation,bool @checked)
        {
            if (@checked && operation.MAC != null)
            {
                CheckedOpenShift();
            }
            CloseShift(operation);
        }
        private void CloseShift(OperationEntiti operation)
        {
            WriteReadXmlFile.WriteXmlFile(operation, new List<OrderEntiti>(), new List<ProductEntiti>(), pathxml);
            if (_signFileContoller.SignFiles(Session.User.KeyPath, Session.User.KeyPassword))
            {
                _serverController.SendZReport(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                    Convert.ToInt32(operation.NumberOfSalesReceipts), operation.FiscalNumberRRO
                    , (bool)AppSettingsManager.GetParameterFiles("TestMode"));
            }
            else
            {
                throw new Exception("невдалося підписати файл");
            }
        }

        public bool OfficialDepositMoney(OperationEntiti operation)
        {
            return OfficialDepositMoneyRecursive(operation, 0, 5);
        }

        private bool OfficialDepositMoneyRecursive(OperationEntiti operation,int depth, int maxDepth)
        {
            try
            {
                if (depth >= maxDepth)
                {
                    throw new Exception("Неможливо виконати операцію");
                }
                SendOfficialDepositMoney(operation);
                return true;
            }
            catch (ExceptionOK)
            {
                SaveDataBase(operation);
                return true;
            }
            catch (ExceptionBadHashPrev exbadHash)
            {
                operation.MAC = exbadHash.Error.Split(" ")[3];
                return OfficialDepositMoneyRecursive(operation,depth+1,maxDepth);
            }
            catch (ExceptionSave exSave)
            {
                MessageBox.Show(exSave.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void SendOfficialDepositMoney(OperationEntiti operation)
        {
            WriteReadXmlFile.WriteXmlFile(operation, new List<OrderEntiti>(), new List<ProductEntiti>(), pathxml);
            if (_signFileContoller.SignFiles(Session.User.KeyPath, Session.User.KeyPassword))
            {
                _serverController.SendServiceCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                    Convert.ToInt32(operation.NumberPayment), operation.FiscalNumberRRO
                    , (bool)AppSettingsManager.GetParameterFiles("TestMode"));
            }
            else
            {
                throw new Exception("невдалося підписати файл");
            }
        }
       
        private void SaveDataBase(OperationEntiti operation)
        {
            operation.User= Session.User;
            _operationRepository.Add(operation);
        }

        private void CheckedOpenShift()
        {
            OperationEntiti operation = _returnDataWithDataBase.GetLastCheck();
            if (operation.NumberPayment == "0" && operation.TypeOperation != 108)
            {
                throw new Exception("Зміна не відкрита");
            }
        }
        private void CheckedCloseShift()
        {
            OperationEntiti operation = _returnDataWithDataBase.GetLastCheck();
            if ((int)operation.NumberOfSalesReceipts == 0)
            {
                throw new Exception("Зміна не закрита");
            }
        }
        
        public string? GetMac()
        {
            return _returnDataWithDataBase.GetMac(pathxml);
        }
        public string? GetLocalNumber()
        {
            return _returnDataWithDataBase.GetLocalNumber();
        }
        public decimal GetNumberFiscalCheck()
        {
            return _returnDataWithDataBase.GetTotalNumberFiscalChechAndReturnFiscalCheck(0);
        }
        public decimal GetNumberReturnFiscalCheck()
        {
            return _returnDataWithDataBase.GetTotalNumberFiscalChechAndReturnFiscalCheck(1);
        }
        public decimal GetTotalFundsReceived()
        {
            return _returnDataWithDataBase.GetTotalSumReceivedAndIssuance(2);
        }
        public decimal GetTotalFundsIssued() 
        {
            return _returnDataWithDataBase.GetTotalSumReceivedAndIssuance(2.01m);
        }
        public decimal GetTotalBuyersAmountCash()
        {
            return _returnDataWithDataBase.GetTotalBuyersAmountAndRestOperation("buyersAmount",0);
        }
        public decimal GetTotalRestCash() 
        {
            return _returnDataWithDataBase.GetTotalBuyersAmountAndRestOperation("restPayment",0);
        }
        public decimal GetTotalBuyersAmountCard()
        {
            return _returnDataWithDataBase.GetTotalBuyersAmountAndRestOperation("buyersAmount", 1);
        }
        public decimal GetTotalRestCard()
        {
            return _returnDataWithDataBase.GetTotalBuyersAmountAndRestOperation("restPayment", 1);
        }
        public decimal GetTotatalChechReturnCash()
        {
            return _returnDataWithDataBase.GetTotalRestReturnOperation(0);
        }
        public decimal GetTotatalChechReturnCard()
        {
            return _returnDataWithDataBase.GetTotalRestReturnOperation(1);
        }

        public void Print(OperationEntiti operation)
        {
            _printingController.PrintCheck(operation);
        }
    }
}
