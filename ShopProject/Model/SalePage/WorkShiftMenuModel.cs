using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Helpers.MiniServiceSigningFile;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ShopProject.Model.SalePage
{
    internal class WorkShiftMenuModel
    {
        private IEntityAccessor<Operation> _operationRepository;
        private IEntityAccessor<GoodsOperation> _goodsOperationRepository;

        private ReturnDataWithDataBase _returnDataWithDataBase;

        string pathxml = "..\\..\\..\\Resource\\BufferStorage\\Chek.xml";

        private MainContoller _mainContoller;
        private FiscalServerController _serverController;
        
        public WorkShiftMenuModel()
        {
            _operationRepository = new OperationTableAccess();
            _goodsOperationRepository = new GoodsOperationTableAccess();

            _returnDataWithDataBase = new ReturnDataWithDataBase();
            _mainContoller = new MainContoller();
            _serverController = new FiscalServerController();
        }

        public bool OpenShift(Operation operation, bool @checked)
        {
           return OpenShiftRecursive(operation, @checked, 0,5);
        }
        private bool OpenShiftRecursive(Operation operation, bool @checked,int depth, int maxDepth)
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
                operation.mac = exbadhash.Error.Split(" ")[3];
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
        private void InspectionOpeningShift(Operation operation, bool @checked)
        {
            if (@checked && operation.mac != null)
            {
                CheckedCloseShift();
            }
            OpenShift(operation);
        }
        private void OpenShift(Operation operation)
        {
            WriteReadXmlFile.WriteXmlFile(operation, new List<GoodsOperation>(), new List<Goods>(), pathxml);
            _mainContoller.SignFiles();
            _serverController.SendServiceCheck(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), Convert.ToInt32(operation.numberPayment), operation.fiscalNumberRRO);
        }

        public bool CloseShift(Operation operation, bool @checked)
        {
           return CloseShiftRecursive(operation, @checked, 0, 5);

        }
        private bool CloseShiftRecursive(Operation operation, bool @checked, int depth, int maxDepth)
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
                operation.mac = exbadHash.Error.Split(" ")[3];
                return CloseShiftRecursive(operation,@checked, depth+1, maxDepth);
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
        private void InspectionEndShift(Operation operation,bool @checked)
        {
            if (@checked && operation.mac != null)
            {
                CheckedOpenShift();
            }
            CloseShift(operation);
        }
        private void CloseShift(Operation operation)
        {
            WriteReadXmlFile.WriteXmlFile(operation, new List<GoodsOperation>(), new List<Goods>(), pathxml);
            _mainContoller.SignFiles();
            _serverController.SendZReport(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), Convert.ToInt32(operation.numberOfSalesReceipts), operation.fiscalNumberRRO);
        }

        public bool OfficialDepositMoney(Operation operation)
        {
            return OfficialDepositMoneyRecursive(operation, 0, 5);
        }

        private bool OfficialDepositMoneyRecursive(Operation operation,int depth, int maxDepth)
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
                operation.mac = exbadHash.Error.Split(" ")[3];
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
        private void SendOfficialDepositMoney(Operation operation)
        {
            WriteReadXmlFile.WriteXmlFile(operation, new List<GoodsOperation>(), new List<Goods>(), pathxml);
            _mainContoller.SignFiles();
            _serverController.SendServiceCheck(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), Convert.ToInt32(operation.numberPayment), operation.fiscalNumberRRO);
        }
       
        private void SaveDataBase(Operation operation)
        {
            _operationRepository.Add(operation);
        }

        private void CheckedOpenShift()
        {
            Operation operation = _returnDataWithDataBase.GetLastCheck();
            if (operation.numberPayment == "0" && operation.typeOperation != 108)
            {
                throw new Exception("Зміна не відкрита");
            }
        }
        private void CheckedCloseShift()
        {
            Operation operation = _returnDataWithDataBase.GetLastCheck();
            if ((int)operation.numberOfSalesReceipts == 0)
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
            return _returnDataWithDataBase.GetTotalNumberFiscalChechAndReturnFiscalCheck(2.01m);
        }
        public decimal GetTotalBuyersAmount()
        {
            return _returnDataWithDataBase.GetTotalBuyersAmountAndRestOperation("buyersAmount");
        }
        public decimal GetTotalRest() 
        {
            return _returnDataWithDataBase.GetTotalBuyersAmountAndRestOperation("restPayment");
        }
    }
}
