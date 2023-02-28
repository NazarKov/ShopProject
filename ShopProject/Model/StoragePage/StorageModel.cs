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
        private ITableRepository<Product, TypeParameterSetTableProduct> _productTableRepository;
        private ITableRepository<ProductArchive,TypeParameterSetTableProductArhive> _arhiveTableRepositories;
        private List<Product> _products;

        public StorageModel()
        {
            _productTableRepository = new ProductTableRepository();
            _arhiveTableRepositories = new ArhiveTableRepositories();
            _products = new List<Product>();
            _products = (List<Product>)_productTableRepository.GetAll();

        }
        public List<Product> GetItems()
        {
            return _products;
        }

        public List<Product>? SearchProduct(string itemSearch, TypeSearch type)
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

        public bool DeleteProduct(Product productDelete)
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

        public void ContertToListProduct(IList list, List<Product> products)
        {
            foreach (var item in list)
            {
                products.Add((Product)item);
            }
        }

        public bool SetProductInArhive(Product item)
        {
            try
            {
                _productTableRepository.SetParameter(item.ID, "arhived", TypeParameterSetTableProduct.Status);
                _arhiveTableRepositories.Add(new ProductArchive() { ID = item.ID ,created_at = DateTime.Now });
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
