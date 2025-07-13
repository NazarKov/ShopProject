 
using ShopProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows; 
using ShopProjectDataBase.DataBase.Model;
using ShopProject.Helpers.FiscalOperationService;

namespace ShopProject.Model.SalePage
{
    internal class ReturnGoodsMenuModel
    {  

        private FiscalOperationController _fiscalOperatonController; 

        public ReturnGoodsMenuModel()
        {
            _fiscalOperatonController = new FiscalOperationController();
        }

        public ProductEntity? Search(string barCode)
        {
            try
            {
                //return _goodsRepository.GetItemBarCode(barCode);
                return new ProductEntity();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public bool SendCheck(List<ProductEntity> products, OperationEntity operation)
        {
            var id = _fiscalOperatonController.SendReturnFiscalCheck(operation, products);
            if (id != string.Empty)
            {
                SaveDataBase(operation, products);
                return true;
            } 
            return false;
        } 

        public string? GetMac() => _fiscalOperatonController.GetMac();

        public string? GetLocalNumber()
        {
            return string.Empty;
            //return _returnDataWithDataBase.GetLocalNumber();
        }

        private void SaveDataBase(OperationEntity operation, List<ProductEntity> goods)
        {
            //_operationRepository.Add(operation);
            //if (goods.Count != 0)
            //{
            //    foreach (ProductEntity item in goods)
            //    {
            //        _goodsOperationRepository.Add(new OrderEntity()
            //        {
            //            Operation = operation,
            //            Goods = item,
            //            Count = (int)item.Count,
            //        });
            //    }
            //}

        }
    }
}
