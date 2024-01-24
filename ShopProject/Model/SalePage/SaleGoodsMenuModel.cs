using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Helpers.HelperForPrinting;
using ShopProject.Helpers.MiniServiceSigningFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ShopProject.Model.SalePage
{
    internal class SaleGoodsMenuModel
    {
        private IEntityAccessor<Goods> _goodsRepository;
        private IEntityAccessor<Operation> _operationRepository;
        private IEntityAccessor<GoodsOperation> _goodsOperationRepository;

        private MainContoller _mainController;
        private FiscalServerController _serverController;
        private PrintingFiscalCheck _printingFiscalCheck;
        private bool _isDrawingChek;

        ReturnDataWithDataBase _returnDataWithDataBase;

        public bool IsDrawinfChek { get { return _isDrawingChek; } set { _isDrawingChek = value; } }


        string pathxml = "..\\..\\..\\Resource\\BufferStorage\\Chek.xml";

        public SaleGoodsMenuModel()
        {
            _goodsRepository = new GoodsTableAccess();
            _operationRepository = new OperationTableAccess();
            _goodsOperationRepository = new GoodsOperationTableAccess();

            _mainController = new MainContoller();
            _serverController = new FiscalServerController();
            _printingFiscalCheck = new PrintingFiscalCheck();
            _returnDataWithDataBase = new ReturnDataWithDataBase();

            _isDrawingChek = true;
        }
        
        public Goods? Search(string barCode)
        {
            try
            {
                return _goodsRepository.GetItemBarCode(barCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        
        public bool SendCheck(List<Goods> goods,Operation operation)
        {
           return SendCheckRecursive(goods,operation,0,5);
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
            catch(ExceptionOK exOK)
            {
                if (exOK.ID != null)
                {
                    SaveDataBase(operation, goods);
                    if (IsDrawinfChek)
                    {
                        PrintCheck(goods, operation, exOK.ID);
                    }
                }
                return true;
            }
            catch (ExceptionBadHashPrev exbadHas)
            {
                operation.mac = exbadHas.Error.Split(" ")[3];
                return SendCheckRecursive(goods, operation,depth+1,maxDepth);
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

        private void SendCheck(Operation operation,List<Goods> goods)
        {
            if (operation.typeOperation == 0)
            {
                 WriteReadXmlFile.WriteXmlFile(operation, new List<GoodsOperation>(),goods, pathxml);
                _mainController.SignFiles();
                _serverController.SendFiscalCheck(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), Convert.ToInt32(operation.numberPayment), operation.fiscalNumberRRO , true);
            }
            else
            {
                SaveDataBase(operation, goods);
            }
        }       
        public void PrintCheck(List<Goods> products, Operation order,string id)
        {
            _printingFiscalCheck.PrintCheck(products,id,order);
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

        public string GetMac()
        {
            return _returnDataWithDataBase.GetMac(pathxml);
        }
       
        public string GetLocalNumber()
        {
            return _returnDataWithDataBase.GetLocalNumber();
        }

    }
}
