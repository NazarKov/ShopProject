using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.ModelRepository;
using ShopProject.Views.StoragePage;

namespace ShopProject.Model.StoragePage
{
    internal class StorageModel
    {
        private ProductTableRepository _productTableRepository;
        private ArhiveTableRepositories _arhiveTableRepositories;
        private List<Product> _products;
        private List<ProductArchive> _archives;

        public StorageModel()
        {
            _productTableRepository = new ProductTableRepository();
            _arhiveTableRepositories = new ArhiveTableRepositories();
            _products = new List<Product>();
            _archives = new List<ProductArchive>();
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
                Validation.ChekIsProductInArhive(item, _archives);//перевірка на існуючий товар в архіві
                _productTableRepository.SetParameter(item.ID, "arhived", TypeParameterSetTable.Status);
                _arhiveTableRepositories.Add(new ProductArchive() { product=item });
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
