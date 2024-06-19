using NPOI.SS.Formula.Functions;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Views.StoragePage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage
{
    internal class OutOfStockModel
    {
        private IEntityAccess<ProductEntiti> _goodsRepository;
        private List<ProductEntiti> _goodsList;
       
        public OutOfStockModel() 
        {
            _goodsList = new List<ProductEntiti>();
            _goodsRepository = new ProductTableAccess();

            try
            {
                //_goodsList = (List<ProductEntiti>)_goodsRepository.GetAll("outStock");    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Erorr",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }
        public List<ProductEntiti> GetAll()
        {
            return _goodsList;
        }

        public List<ProductEntiti>? SearchGoods(string itemSearch )
        {
            try
            {
                //return Search.GoodsDataBase(itemSearch, (List<ProductEntiti>)_goodsRepository.GetAll("outStock"));
                return new List<ProductEntiti>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return null;
            }
        }
        public void ConvertToList(IList collection , List<ProductEntiti> itemConver)
        {
            foreach (ProductEntiti item in collection)
            {
                itemConver.Add(item);
            }
        }

        public bool ReturnGoodsInStorage(ProductEntiti goodsOutOfStock)
        {
            try
            {
                //_goodsRepository.UpdateParameter(goodsOutOfStock.id, "status", "in_stock");
                //_goodsRepository.UpdateParameter(goodsOutOfStock.id, "outStock", null);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }

        public bool DeleteRecordArhive(ProductEntiti goods)
        {
            try 
            {
                _goodsRepository.Delete(goods);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }

    }
}
