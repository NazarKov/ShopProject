using NPOI.SS.Formula.Functions;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.ModelRepository;
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
        private ITableRepository<ProductsOutOfStock, TypeParameterSetTableOutOfStock> _outOfStockPeoductRepository;
        private ITableRepository<Product,TypeParameterSetTableProduct> _productRepository;
       
        public OutOfStockModel() 
        {
            _outOfStockPeoductRepository = new OutOfStockTableRepositories();
            _productRepository = new ProductTableRepository();
        }
        public List<ProductsOutOfStock> GetAll()
        {
            return (List<ProductsOutOfStock>)_outOfStockPeoductRepository.GetAll();
        }

        public List<ProductsOutOfStock>? SearchProducts(string itemSearch, TypeSearch type)
        {
            try
            {
                return Search.OutOfStockProductDataBase(itemSearch, type, (List<ProductsOutOfStock>)_outOfStockPeoductRepository.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return null;
            }
        }
        public void ConvertToList(IList collection, List<ProductsOutOfStock> itemConver)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                itemConver.Add((ProductsOutOfStock)collection[i]);
                itemConver[i].Product = ((ProductsOutOfStock)collection[i]).Product;
            }
        }

        public bool ReturnProductInStorage(ProductsOutOfStock productsOutOfStock)
        {
            try
            {
                _productRepository.SetParameter(productsOutOfStock.ID, "in_stock", TypeParameterSetTableProduct.Status);
                _outOfStockPeoductRepository.Delete(productsOutOfStock);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }

        public bool DeleteRecordArhive(ProductsOutOfStock productOutOfStock, Product product)
        {
            try
            {
                _outOfStockPeoductRepository.Delete(productOutOfStock);
                _productRepository.Delete(product);
                return true;
                throw new Exception("Помилка видалення");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }

    }
}
