using NPOI.SS.Formula.Functions;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
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
        private IEntityAccessor<Goods> _goodsRepository;
        private List<Goods> _goodsList;
       
        public OutOfStockModel() 
        {
            _goodsList = new List<Goods>();
            _goodsRepository = new GoodsTableAccess();

            try
            {
                _goodsList = (List<Goods>)_goodsRepository.GetAll("outStock");    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Erorr",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }
        public List<Goods> GetAll()
        {
            return _goodsList;
        }

        public List<Goods>? SearchGoods(string itemSearch, TypeSearch type)
        {
            try
            {
                return Search.GoodsDataBase(itemSearch, type, (List<Goods>)_goodsRepository.GetAll("outStock"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return null;
            }
        }
        public void ConvertToList(IList collection , List<Goods> itemConver)
        {
            foreach (Goods item in collection)
            {
                itemConver.Add(item);
            }
        }

        public bool ReturnGoodsInStorage(Goods goodsOutOfStock)
        {
            try
            {
                _goodsRepository.UpdateParameter(goodsOutOfStock.id, "status", "in_stock");
                _goodsRepository.UpdateParameter(goodsOutOfStock.id, "outStock", null);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }

        public bool DeleteRecordArhive(Goods goods)
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
