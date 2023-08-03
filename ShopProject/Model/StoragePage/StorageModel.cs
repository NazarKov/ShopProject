using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;

namespace ShopProject.Model.StoragePage
{
    internal class StorageModel
    {
        private IEntityAccessor<Goods> _goodsRepository;
        //private IEntityAccessor<GoodsArchive, TypeParameterSetTableProductArhive> _arhiveTableRepositories;
        //private IEntityAccessor<GoodsOutOfStock, TypeParameterSetTableOutOfStock> _outOfStockRepositories;
        private List<Goods> _goods;

        public StorageModel()
        { 
            _goodsRepository = new GoodsTableAccess();
            //_arhiveTableRepositories = new ArhiveTableRepositories();
            //_outOfStockRepositories = new OutOfStockTableRepositories();

            try
            {
                _goods = new List<Goods>();
                _goods = (List<Goods>)_goodsRepository.GetAll();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public List<Goods> GetItems()
        {
            return _goods;
        }

        public List<Goods>? SearchProduct(string itemSearch, TypeSearch type)
        {
            try
            {
                return null;
                //return Search.ProductDataBase(itemSearch, type, _products);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return null;
            }
        }

        public bool DeleteProduct(Goods productDelete)
        {
            try
            {
                //_productTableRepository.Delete(productDelete);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public void ContertToListProduct(IList list, List<Goods> products)
        {
            foreach (var item in list)
            {
                products.Add((Goods)item);
            }
        }

        public bool SetProductInArhive(Goods item)
        {
            try
            {
                //_productTableRepository.SetParameter( item.id, "arhived", TypeParameterSetTableProduct.Status);
                //_arhiveTableRepositories.Add(new GoodsArchive() { id = item.id ,createdAt = DateTime.Now });
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }
        public bool SetProductinOutOfStok(Goods item)
        {
            try
            {
            ////    _productTableRepository.SetParameter(item.id, "out_of_stock", TypeParameterSetTableProduct.Status);
            ////    _outOfStockRepositories.Add(new GoodsOutOfStock() { ID=item.id,createdAt = DateTime.Now});
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }
    }
}
