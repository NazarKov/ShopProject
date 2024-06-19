using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;

namespace ShopProject.Model.StoragePage
{

    internal class StorageModel
    {
        private IEntityGet<ProductEntiti> _productsGet;
        private IEntityUpdate<ProductEntiti> _productsUpdate;
        private IEntityAccess<ProductEntiti> _productsRepository;

        private static List<ProductEntiti>? _products;

        public StorageModel()
        {
            _products = new List<ProductEntiti>();
            _productsGet = new ProductTableAccess();
            _productsUpdate = new ProductTableAccess();
            _productsRepository = new ProductTableAccess();

            new Thread(new ThreadStart(ChekedNull)).Start();
        }

        public List<ProductEntiti> SearchItems(string item) 
        {
            try
            {
                _products = (List<ProductEntiti>)_productsGet.GetAll("in_stock");
                if (item != "")
                {
                    return Search(item, _products);
                }
                return _products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return new List<ProductEntiti>();
            }
        }

        private List<ProductEntiti> Search(string item, List<ProductEntiti> _productsList)
        {
            var  result = new  List<ProductEntiti>();

            var count = _productsList.Count; 
            for (int i = 0; i < count; i++)
            {
                if (_productsList[i].Code.Contains(item))
                {
                    result.Add(_productsList[i]);
                }
                else if (_productsList[i].NameProduct.ToLower().Contains(item.ToLower()))
                {
                    result.Add(_productsList[i]);
                }
                else if (_productsList[i].Articule.ToLower().ToLower().Contains(item.ToLower()))
                {
                    result.Add(_productsList[i]);
                }
            }
            return result;
        }

        public int GetCount()
        {
            try
            {
                return _productsGet.GetAll("in_stock").Count();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return 0;
            }
        }

        public bool DeleteItem(ProductEntiti item)
        {
            try
            {
                _productsRepository.Delete(item);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public bool SetItemInArhive(ProductEntiti item)
        {
            try
            {
                _productsUpdate.UpdateParameter(item.ID, nameof(item.Status), "arhived");
                _productsUpdate.UpdateParameter(item.ID, nameof(item.ArhivedAt), DateTime.Now);
                _productsUpdate.UpdateParameter(item.ID, nameof(item.Count), 0);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }
        public bool SetItemOutOfStock(ProductEntiti item)
        {
            try
            {
                _productsUpdate.UpdateParameter(item.ID, nameof(item.Status), "outStock");
                _productsUpdate.UpdateParameter(item.ID, nameof(item.OutStockAt), DateTime.Now);
                _productsUpdate.UpdateParameter(item.ID, nameof(item.Count), 0);

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public void ContertIListToList(IList list, List<ProductEntiti> _products)
        {
            foreach(ProductEntiti item in list)
            {
                _products.Add(item);
            }
        }

        private void ChekedNull()
        {
            Parallel.ForEach(_productsGet.GetAll("in_stock"), item =>
            {
                if (item.Count <= 0)
                {
                    SetItemOutOfStock(item);
                }
            });

        }
    }
}
