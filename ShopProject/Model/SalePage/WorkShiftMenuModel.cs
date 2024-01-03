using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Helpers.MiniServiceSigningFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ShopProject.Model.SalePage
{
    internal class WorkShiftMenuModel
    {
        private IEntityAccessor<Operation> _operationRepository;
        private IEntityAccessor<GoodsOperation> _goodsOperationRepository;


        string pathxml = "..\\..\\..\\Resource\\BufferStorage\\Chek.xml";

        private MainContoller _mainContoller;
        private FiscalServerController _serverController;
        
        public WorkShiftMenuModel()
        {
            _operationRepository = new OperationTableAccess();
            _goodsOperationRepository = new GoodsOperationTableAccess();


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
            RecordingOperationXmlFile.WriteXmlFile(operation, new List<Goods>(), true, pathxml);
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
            RecordingOperationXmlFile.WriteXmlFile(operation, new List<Goods>(), false, pathxml);
            _mainContoller.SignFiles();
            _serverController.SendZReport(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), Convert.ToInt32(operation.numberOfSalesReceipts), operation.fiscalNumberRRO);
        }


        private void SaveDataBase(Operation operation)
        {
            _operationRepository.Add(operation);
        }

        private void CheckedOpenShift()
        {
            Operation operation = GetLastCheck();
            if (operation.numberPayment == "0" && operation.typeOperation != 108)
            {
                throw new Exception("Зміна не відкрита");
            }
        }
        private void CheckedCloseShift()
        {
            Operation operation = GetLastCheck();
            if ((int)operation.numberOfSalesReceipts == 0)
            {
                throw new Exception("Зміна не закрита");
            }
        }
        
        public string? GetMac(bool typecheck)
        {
            try
            {
                Operation operation = GetLastCheck();

                List<GoodsOperation> goodsOperation = _goodsOperationRepository.GetAll().Where(item => item.operation.id == operation.id).ToList();
                List<Goods> goodsList = new List<Goods>();
                RecordingOperationXmlFile.WriteXmlFile(operation, goodsOperation, typecheck, pathxml);
                return SHA.GenerateSHA256File(pathxml);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public string? GetLocalNumber()
        {
            try
            {
                Operation operation = GetLastCheck();
                return (Convert.ToInt32(operation.numberPayment) + 1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        private Operation GetLastCheck()
        {
            var operationList = _operationRepository.GetAll();
            if (operationList != null)
            {
                return operationList.ElementAt(operationList.Count() - 1);
            }
            else
            {
                throw new Exception("Зачекайте");
            }
        }
        

    }


}
