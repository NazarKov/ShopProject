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
using System.Linq;
using System.Windows;

namespace ShopProject.Model.SalePage
{
    internal class SaleGoodsMenuModel
    {
        private IEntityGet<ProductEntiti> _goodsRepository;
        private IEntityAccess<OperationEntiti> _operationRepository;
        private IEntityAccess<OrderEntiti> _goodsOperationRepository;

        private SigningFileContoller _mainController;
        private FiscalServerController _serverController;
        private PrintingFiscalCheck _printingFiscalCheck;
        private bool _isDrawingChek;

        ReturnDataWithDataBase _returnDataWithDataBase;

        public bool IsDrawinfChek { get { return _isDrawingChek; } set { _isDrawingChek = value; } }


        string pathxml = "C:\\ProgramData\\ShopProject\\Temp\\Chek.xml";

        public SaleGoodsMenuModel()
        {
            _goodsRepository = new ProductTableAccess();
            _operationRepository = new OperationTableAccess();
            _goodsOperationRepository = new OrderTableAccess();

            _mainController = new SigningFileContoller();
            _serverController = new FiscalServerController();
            _printingFiscalCheck = new PrintingFiscalCheck();
            _returnDataWithDataBase = new ReturnDataWithDataBase();

            _isDrawingChek = true;
        }
        
        public ProductEntiti? Search(string barCode)
        {
            try
            {
                return _goodsRepository.GetByBarCode(barCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        
        public bool SendCheck(List<ProductEntiti> goods,OperationEntiti operation)
        {
           return SendCheckRecursive(goods,operation,0,5);
        }     
        private bool SendCheckRecursive(List<ProductEntiti> goods, OperationEntiti operation, int depth, int maxDepth)
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
                operation.MAC = exbadHas.Error.Split(" ")[3];
                return SendCheckRecursive(goods, operation,depth+1,maxDepth);
            }
            catch (ExceptionCheck exCheck)
            {
                MessageBox.Show(exCheck.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            catch (ExceptionSave exSave)
            {
                operation.MAC = string.Empty;
                return SendCheckRecursive(goods, operation, depth + 1, maxDepth);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }

        private void SendCheck(OperationEntiti operation,List<ProductEntiti> goods)
        {
            if (operation.TypeOperation == 0)
            {
                 WriteReadXmlFile.WriteXmlFile(operation, new List<OrderEntiti>(),goods, pathxml);
                _mainController.SignFiles(Session.User.KeyPath,Session.User.KeyPassword);
                _serverController.SendFiscalCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss"))
                    , Convert.ToInt32(operation.NumberPayment), operation.FiscalNumberRRO
                    , (bool)AppSettingsManager.GetParameterFiles("TestMode"));
            }
            else
            {
                SaveDataBase(operation, goods);
            }
        }       
        public void PrintCheck(List<ProductEntiti> products, OperationEntiti order,string id)
        {
            _printingFiscalCheck.PrintCheck(products, id, order);
        }
       

        private void SaveDataBase(OperationEntiti operation, List<ProductEntiti> goods)
        {
            operation.User = Session.User;
            _operationRepository.Add(operation);
            if (goods.Count != 0)
            {
                foreach (ProductEntiti item in goods)
                {
                    _goodsOperationRepository.Add(new OrderEntiti()
                    {
                        Operation = operation,
                        Goods = item,
                        Count = (int)item.Count,
                       
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
