using FiscalServerApi.ExceptionServer;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FiscalServerApi;
using ShopProject.DataBase.Interfaces;
using ShopProject.Helpers.MiniServiceSigningFile;
using ShopProject.DataBase.DataAccess.EntityAccess;

namespace ShopProject.Model.SalePage
{
    internal class ReturnGoodsMenuModel
    {

        private IEntityAccessor<Goods> _goodsRepository;
        private IEntityAccessor<Operation> _operationRepository;
        private IEntityAccessor<GoodsOperation> _goodsOperationRepository;

        private ReturnDataWithDataBase _returnDataWithDataBase;

        private MainContoller _mainController;
        private FiscalServerController _serverController;
        string pathxml = "..\\..\\..\\Resource\\BufferStorage\\Chek.xml";


        public ReturnGoodsMenuModel()
        {
            _goodsRepository = new GoodsTableAccess();
            _operationRepository = new OperationTableAccess();
            _goodsOperationRepository = new GoodsOperationTableAccess();
            _returnDataWithDataBase = new ReturnDataWithDataBase();


            _mainController = new MainContoller();
            _serverController = new FiscalServerController();
        }

        public Goods? Search(string barCode)
        {
            try
            {
                return _goodsRepository.GetItemBarCode(barCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public bool SendCheck(List<Goods> goods, Operation operation)
        {
            return SendCheckRecursive(goods, operation, 0, 5);
        }
        private bool SendCheckRecursive(List<Goods> goods, Operation operation, int depth, int maxDepth)
        {
            try
            {
                if (depth >= maxDepth)
                {
                    throw new Exception("Неможливо виконати операцію");
                }
                SendCheck(operation, goods);
                return true;
            }
            catch (ExceptionOK exOK)
            {
                if (exOK.ID != null)
                {
                    SaveDataBase(operation, goods);
                }
                return true;
            }
            catch (ExceptionBadHashPrev exbadHas)
            {
                operation.mac = exbadHas.Error.Split(" ")[3];
                return SendCheckRecursive(goods, operation, depth + 1, maxDepth);
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

        private void SendCheck(Operation operation, List<Goods> goods)
        {
            if (operation.typeOperation == 1)
            {
                WriteReadXmlFile.WriteXmlFile(operation, new List<GoodsOperation>(), goods, pathxml);
                _mainController.SignFiles();
                _serverController.SendFiscalCheck(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), Convert.ToInt32(operation.numberPayment), operation.fiscalNumberRRO, true);
            }
            else
            {
                SaveDataBase(operation, goods);
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
    }
}
