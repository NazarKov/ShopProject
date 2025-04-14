using FiscalServerApi.ExceptionServer;
using ShopProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FiscalServerApi;

namespace ShopProject.Model.SalePage
{
    internal class ReturnGoodsMenuModel
    {

        //private IEntityAccess<ProductEntiti> _goodsRepository;
        //private IEntityAccess<OperationEntiti> _operationRepository;
        //private IEntityAccess<OrderEntiti> _goodsOperationRepository;

        private ReturnDataWithDataBase _returnDataWithDataBase;

        //private MainContollerTcp _mainController;
        private FiscalServerController _serverController;
        string pathxml = "..\\..\\..\\Resource\\BufferStorage\\Chek.xml";


        public ReturnGoodsMenuModel()
        {
            //_goodsRepository = new ProductTableAccess();
            //_operationRepository = new OperationTableAccess();
            //_goodsOperationRepository = new OrderTableAccess();
            //_returnDataWithDataBase = new ReturnDataWithDataBase();


            //_mainController = new MainContollerTcp();
            _serverController = new FiscalServerController();
        }

        //public ProductEntiti? Search(string barCode)
        //{
        //    try
        //    {
        //        //return _goodsRepository.GetItemBarCode(barCode);
        //        return new ProductEntiti();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return null;
        //    }
        //}

        //public bool SendCheck(List<ProductEntiti> goods, OperationEntiti operation)
        //{
        //    return SendCheckRecursive(goods, operation, 0, 5);
        //}
        //private bool SendCheckRecursive(List<ProductEntiti> goods, OperationEntiti operation, int depth, int maxDepth)
        //{
        //    try
        //    {
        //        if (depth >= maxDepth)
        //        {
        //            throw new Exception("Неможливо виконати операцію");
        //        }
        //        SendCheck(operation, goods);
        //        return true;
        //    }
        //    catch (ExceptionOK exOK)
        //    {
        //        if (exOK.ID != null)
        //        {
        //            SaveDataBase(operation, goods);
        //        }
        //        return true;
        //    }
        //    catch (ExceptionBadHashPrev exbadHas)
        //    {
        //        operation.MAC = exbadHas.Error.Split(" ")[3];
        //        return SendCheckRecursive(goods, operation, depth + 1, maxDepth);
        //    }
        //    catch (ExceptionCheck exCheck)
        //    {
        //        MessageBox.Show(exCheck.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
        //        return false;
        //    }
        //    catch (ExceptionSave exSave)
        //    {
        //        MessageBox.Show(exSave.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
        //        return false;
        //    }
        //}

        //private void SendCheck(OperationEntiti operation, List<ProductEntiti> goods)
        //{
        //    if (operation.TypeOperation == 1)
        //    {
        //        //WriteReadXmlFile.WriteXmlFile(operation, new List<OrderEntiti>(), goods, pathxml);
        //        //_mainController.SignFiles();
        //        _serverController.SendFiscalCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")), Convert.ToInt32(operation.NumberPayment), operation.FiscalNumberRRO, true);
        //    }
        //    else
        //    {
        //        SaveDataBase(operation, goods);
        //    }
        //}
        //public string? GetMac()
        //{
        //    return _returnDataWithDataBase.GetMac(pathxml);
        //}

        //public string? GetLocalNumber()
        //{
        //    return _returnDataWithDataBase.GetLocalNumber();
        //}

        //private void SaveDataBase(OperationEntiti operation, List<ProductEntiti> goods)
        //{
        //    _operationRepository.Add(operation);
        //    if (goods.Count != 0)
        //    {
        //        foreach (ProductEntiti item in goods)
        //        {
        //            _goodsOperationRepository.Add(new OrderEntiti()
        //            {
        //                Operation = operation,
        //                Goods = item,
        //                Count = (int)item.Count,
        //            });
        //        }
        //    }

        //}
    }
}
