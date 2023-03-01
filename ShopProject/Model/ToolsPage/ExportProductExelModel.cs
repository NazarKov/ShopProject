using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.ModelRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class ExportProductExelModel
    {
        private ITableRepository<Product, TypeParameterSetTableProduct> _productRepository;
        private FileExel? fileExel;

        public ExportProductExelModel()
        {
            _productRepository = new ProductTableRepository();
        }

        public Product? GetItem(string itemSearch)
        {
            Product product = (Product)_productRepository.GetItem(itemSearch);
            return product;
        }

        public List<Product> GetItems()
        {
            return (List<Product>)_productRepository.GetAll();
        }
        
        public bool Export(string path,List<Product> products)
        {
            try
            {
                fileExel = new FileExel();
                fileExel.Write(path,products);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }
    }
}
