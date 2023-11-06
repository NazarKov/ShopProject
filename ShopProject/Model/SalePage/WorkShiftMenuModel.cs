using FiscalServerApi;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Helpers.MiniServiceSigningFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.SalePage
{
    internal class WorkShiftMenuModel
    {
        private IEntityAccessor<Goods> _goodsRepository;
        private IEntityAccessor<Operation> _operationRepository;
        private IEntityAccessor<GoodsOperation> _goodsOperationRepository;


        string pathxml = "..\\..\\..\\Resource\\BufferStorage\\Chek.xml";

        private MainContoller _mainContoller;
        private FiscalServerController _serverController;
        
        public WorkShiftMenuModel()
        {
            _goodsRepository = new GoodsTableAccess();
            _operationRepository = new OperationTableAccess();
            _goodsOperationRepository = new GoodsOperationTableAccess();


            _mainContoller = new MainContoller();
            _serverController = new FiscalServerController();
        }
        public string GetMac(bool typechek)
        {
            try
            {
                Operation operation = GetLastChek();

                List<GoodsOperation> goodsOperation = _goodsOperationRepository.GetAll().Where(item => item.operation.id == operation.id).ToList();
                List<Goods> goodsList = new List<Goods>();
                RecordingOperationXmlFile.WriteXmlFile(operation, goodsOperation, typechek, pathxml);
                return SHA.GenerateSHA256File(pathxml);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string GetLocalNumber()
        {
            try
            {
                Operation operation = GetLastChek();
                return (Convert.ToInt32(operation.numberPayment) + 1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void OpenChange(Operation operation, bool cheked)
        {
            try
            {
                if (cheked && operation.mac != null)
                {
                    ChekedCloseChange();
                }
                if (operation.mac != null)
                {
                    OpenChange(operation);
                }
                else
                {
                    operation.mac = adjustmentHash(operation, "servise");
                    OpenChange(operation);
                }

            }
            catch (Exception ex)
            {
                try
                {
                    if (ex.Message == "Невірний хеш попереднього чеку")
                    {
                        operation.mac = adjustmentHash(operation, "servise");
                        OpenChange(operation);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void OpenChange(Operation operation)
        {
            RecordingOperationXmlFile.WriteXmlFile(operation, new List<Goods>(), true, pathxml);
            SingFile();
            if (_serverController.SendServiceCheck(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), Convert.ToInt32(operation.numberPayment), operation.fiscalNumberRRO) != null)
            {
                SaveDataBase(operation, new List<Goods>());
                MessageBox.Show("зміна відкрита", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void CloseChange(Operation operation)
        {
            try
            {
                ChekedOpenChange();
                closeChange(operation);
            }
            catch (Exception ex)
            {
                try
                {
                    if (ex.Message == "Невірний хеш попереднього чеку")
                    {
                        operation.mac = adjustmentHash(operation, "zreport");
                        closeChange(operation);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void closeChange(Operation operation)
        {
            RecordingOperationXmlFile.WriteXmlFile(operation, new List<Goods>(), false, pathxml);

            SingFile();
            if (_serverController.SendZreport(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), Convert.ToInt32(operation.numberOfSalesReceipts), operation.fiscalNumberRRO))
            {
                SaveDataBase(operation, new List<Goods>());
                MessageBox.Show("Зміна закрита", "information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SaveDataBase(Operation operation, List<Goods> goods)
        {
            _operationRepository.Add(operation);
            if (goods.Count != 0)
            {
                foreach (Goods item in goods)
                {
                    _goodsOperationRepository.Add(new GoodsOperation()
                    {
                        operation = operation,
                        goods = item,
                        count = (int)item.count,
                    });
                }
            }
        }

        private void SingFile()
        {
            _mainContoller.StartServise();
            _mainContoller.ConnectService();
            _mainContoller.SendingCommand(TypeCommand.Initialize);
            _mainContoller.SendingCommand(TypeCommand.SingFile);
            _mainContoller.SendingCommand(TypeCommand.Disconnect);

        }
        private void ChekedOpenChange()
        {
            Operation operation = GetLastChek();
            if (operation.numberPayment == "0" && operation.typeOperation != 108)
            {
                throw new Exception("Зміна не відкрита");
            }
        }
        private void ChekedCloseChange()
        {
            Operation operation = GetLastChek();
            if ((int)operation.numberOfSalesReceipts == 0)
            {
                throw new Exception("Зміна не закрита");
            }
        }
        private Operation GetLastChek()
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
        private string adjustmentHash(Operation operation, string type)
        {
            try
            {

                switch (type)
                {
                    case "servise":
                        {
                            RecordingOperationXmlFile.WriteXmlFile(operation, new List<Goods>(), true, pathxml);
                            SingFile();
                           // return _serverController.SendServiseChekAdjustmentHash(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), operation.fiscalNumberRRO);
                            break;
                        }
                    case "chek":
                        {
                            RecordingOperationXmlFile.WriteXmlFile(operation, new List<Goods>(), true, pathxml);
                            SingFile();
                           // return _serverController.SendChekAdjustmentHash(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), operation.fiscalNumberRRO);
                            break;
                        }
                    case "zreport":
                        {
                            RecordingOperationXmlFile.WriteXmlFile(operation, new List<Goods>(), false, pathxml);
                            SingFile();
                            //return _serverController.SendZtreportAdjustmentHash(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), operation.fiscalNumberRRO);
                            break;
                        }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }

        }

    }


}
