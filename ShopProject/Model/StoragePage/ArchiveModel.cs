using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace ShopProject.Model.StoragePage
{

    internal class ArchiveModel
    {
        private IEntityAccessor<Goods> _goodsRepository;
        private List<Goods> _arhiveGoods;

        public ArchiveModel()
        {
            _goodsRepository = new GoodsTableAccess();
            _arhiveGoods = new List<Goods>();
            try
            {
                _arhiveGoods = (List<Goods>)_goodsRepository.GetAll("arhived");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public List<Goods> GetItems()
        {
            return _arhiveGoods;
        }

        public List<Goods>? SearchArhive(string itemSearch)
        {
            try
            {
                return Search.GoodsDataBase(itemSearch, (List<Goods>) _goodsRepository.GetAll("arhived"));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return null;
            }
        }
        public bool DeleteRecordArhive(Goods product)
        {
            try
            {
                _goodsRepository.Delete(product);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }
        public void ConvertToList(IList collection, List<Goods> itemConver)
        {
            foreach(Goods item in collection)
            {
                itemConver.Add(item);
            }
        }
        
        public bool ReturnGoodsInStorage(Goods archive)
        {
            try
            {
                _goodsRepository.UpdateParameter(archive.id, "status", "in_stock");
                _goodsRepository.UpdateParameter(archive.id, "arhived", null);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButton.OK);
                return false;
            }
        }
    }
}
