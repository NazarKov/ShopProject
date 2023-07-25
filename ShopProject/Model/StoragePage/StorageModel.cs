using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.ModelRepository;

namespace ShopProject.Model.StoragePage
{
    internal class StorageModel
    {
        private ITableRepository<Goods, TypeParameterSetTableProduct> _productTableRepository;
        private ITableRepository<GoodsArchive,TypeParameterSetTableProductArhive> _arhiveTableRepositories;
        private ITableRepository<GoodsOutOfStock, TypeParameterSetTableOutOfStock> _outOfStockRepositories;
        private List<Goods> _products;

        public StorageModel()
        {
            _productTableRepository = new ProductTableRepository();
            _arhiveTableRepositories = new ArhiveTableRepositories();
            _outOfStockRepositories = new OutOfStockTableRepositories();

            _products = new List<Goods>();
            _products = (List<Goods>)_productTableRepository.GetAll();

        }
        public List<Goods> GetItems()
        {
            return _products;
        }

        public List<Goods>? SearchProduct(string itemSearch, TypeSearch type)
        {
            try
            {
                return Search.ProductDataBase(itemSearch, type, _products);
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
                _productTableRepository.Delete(productDelete);
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
                _productTableRepository.SetParameter(item.ID, "arhived", TypeParameterSetTableProduct.Status);
                _arhiveTableRepositories.Add(new GoodsArchive() { ID = item.ID ,created_at = DateTime.Now });
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
                _productTableRepository.SetParameter(item.ID, "out_of_stock", TypeParameterSetTableProduct.Status);
                _outOfStockRepositories.Add(new GoodsOutOfStock() { ID=item.ID,created_at = DateTime.Now});
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
